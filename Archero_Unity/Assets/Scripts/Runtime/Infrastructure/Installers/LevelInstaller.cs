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
  public class LevelInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private Transform _charactersParent;
    [SerializeField] private HeroSpawnPoint _heroSpawnPoint;
    [Inject] private IBattleStarter _battleStarter;
    [Inject] private ICharacterRegistry _characterRegistry;
    [Inject] private ICurtainService _curtainService;
    [Inject] private IEnemyAttackHandlerBuilder _enemyAttackHandlerBuilder;
    [Inject] private IInputService _inputService;
    [Inject] private IPauseService _pauseService;
    [Inject] private ITargetPicker _targetPicker;
    [Inject] private IVisibilityService _visibilityService;

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public async void Initialize()
    {
      HeroBehaviour hero = Container.Resolve<HeroFactory>().Create(_heroSpawnPoint, _charactersParent);
      _characterRegistry.RegisterHero(hero);

      EnemySpawnPoint[] enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
      foreach (EnemySpawnPoint enemySpawnPoint in enemySpawnPoints)
      {
        EnemyBehaviour enemy = Container.Resolve<EnemyFactory>().Create(enemySpawnPoint, _charactersParent);
        _characterRegistry.RegisterEnemy(enemy);
      }

      _curtainService.Hide();
      await _battleStarter.WaitForBattleStart();

      InitializeBattle(hero);
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
    }

    private void InitializeBattle(HeroBehaviour hero)
    {
      _targetPicker.Initialize();
      InitializeHero(hero);
      foreach (EnemyBehaviour enemy in _characterRegistry.Enemies)
        enemy.Initialize(_characterRegistry, _pauseService, _visibilityService, _enemyAttackHandlerBuilder);
    }

    private void InitializeHero(HeroBehaviour hero)
    {
      hero.Initialize(_heroSpawnPoint.Config, _inputService, _pauseService, _targetPicker);
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