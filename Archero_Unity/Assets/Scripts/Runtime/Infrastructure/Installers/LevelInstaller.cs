using System;
using System.Linq;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class LevelInstaller : MonoInstaller, IInitializable, IDisposable
  {
    [SerializeField] private Transform _charactersParent;
    [SerializeField] private HeroSpawnPoint _heroSpawnPoint;
    [Inject] private IBattleStarter _battleStarter;
    [Inject] private ICurtainService _curtainService;
    [Inject] private IInputService _inputService;
    [Inject] private IPauseService _pauseService;

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public async void Initialize()
    {
      var characterRegistry = Container.Resolve<ICharacterRegistry>();
      HeroBehaviour hero = Container.Resolve<HeroFactory>().Create(_heroSpawnPoint, _charactersParent);
      characterRegistry.RegisterHero(hero);

      EnemySpawnPoint[] enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
      foreach (EnemySpawnPoint enemySpawnPoint in enemySpawnPoints)
      {
        EnemyBehaviour enemy = Container.Resolve<EnemyFactory>().Create(enemySpawnPoint, _charactersParent);
        characterRegistry.RegisterEnemy(enemy);
      }

      _curtainService.Hide();
      await _battleStarter.WaitForBattleStart();

      InitializeBattle(hero, characterRegistry);
    }

    public void Dispose()
    {
      var targetPicker = Container.TryResolve<ITargetPicker>();
      var characterRegistry = Container.TryResolve<ICharacterRegistry>();
      var heroAttackSystem = Container.TryResolve<IHeroAttackSystem>();
      heroAttackSystem?.Dispose();
      characterRegistry?.Dispose();
      _pauseService?.Dispose();
      targetPicker?.Dispose();
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<LevelInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind<HeroFactory>()
        .AsSingle();

      Container
        .Bind<EnemyFactory>()
        .AsSingle();

      Container
        .Bind<HeroAttackHandlerFactory>()
        .AsSingle();

      Container
        .Bind<DamageApplierFactory>()
        .AsSingle();

      Container
        .Bind<IHeroAttackSystem>()
        .To<HeroAttackSystem>()
        .AsSingle();

      Container
        .Bind<IEnemyAttackHandlerBuilder>()
        .To<EnemyAttackHandlerBuilder>()
        .AsSingle();

      Container
        .Bind<ITargetPicker>()
        .To<HeroTargetPicker>()
        .AsSingle();

      Container
        .Bind<ICharacterRegistry>()
        .To<CharacterRegistry>()
        .AsSingle();

      Container
        .Bind<IVisibilityService>()
        .To<VisibilityService>()
        .AsSingle();
    }

    private void InitializeBattle(HeroBehaviour hero, ICharacterRegistry characterRegistry)
    {
      var enemyAttackHandlerBuilder = Container.Resolve<IEnemyAttackHandlerBuilder>();
      var visibilityService = Container.Resolve<IVisibilityService>();
      InitializeHero(hero);
      foreach (EnemyBehaviour enemy in characterRegistry.Enemies)
        enemy.Initialize(characterRegistry, _pauseService, visibilityService, enemyAttackHandlerBuilder);
    }

    private void InitializeHero(HeroBehaviour hero)
    {
      var targetPicker = Container.Resolve<ITargetPicker>();
      targetPicker.Initialize();
      hero.Initialize(_heroSpawnPoint.Config, _inputService, _pauseService, targetPicker);
      IHeroAttackHandler heroDefaultAttackHandler =
        Container.Resolve<HeroAttackHandlerFactory>().Create(_heroSpawnPoint.Config, hero);
      IDamageApplier defaultHeroDamageApplier =
        Container.Resolve<DamageApplierFactory>().Create(_heroSpawnPoint.Config.DefaultDamageType);

      var heroAttackSystem = Container.Resolve<IHeroAttackSystem>();
      heroAttackSystem.AddAttackHandler(heroDefaultAttackHandler);
      heroAttackSystem.AddDamageApplier(defaultHeroDamageApplier);
      heroAttackSystem.StartWorking();
    }
  }
}