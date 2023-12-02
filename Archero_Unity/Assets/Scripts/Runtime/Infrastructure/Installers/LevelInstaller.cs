using System.Linq;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class LevelInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private Transform _charactersParent;
    [SerializeField] private HeroSpawnPoint _heroSpawnPoint;

    private ICharacterRegistry _characterRegistry;
    private ITargetPicker _targetPicker;
    private IInputService _inputService;
    private IGameplayPrefabProvider _gameplayPrefabProvider;
    private IVisibilityService _visibilityService;
    private IBattleStarter _battleStarter;
    private TransformContainer _transformContainer;

    [Inject]
    private void Construct(IInputService inputService, ITargetPicker targetPicker, ICharacterRegistry characterRegistry,
      IGameplayPrefabProvider gameplayPrefabProvider, IVisibilityService visibilityService,
      IBattleStarter battleStarter,
      TransformContainer transformContainer)
    {
      _inputService = inputService;
      _targetPicker = targetPicker;
      _characterRegistry = characterRegistry;
      _gameplayPrefabProvider = gameplayPrefabProvider;
      _visibilityService = visibilityService;
      _battleStarter = battleStarter;
      _transformContainer = transformContainer;
    }

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public async void Initialize()
    {
      Debug.Log("Level initialized");
      HeroBehaviour hero = Container.Resolve<HeroFactory>().Create(_heroSpawnPoint, _charactersParent);
      _characterRegistry.RegisterHero(hero);

      EnemySpawnPoint[] enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
      foreach (EnemySpawnPoint enemySpawnPoint in enemySpawnPoints)
      {
        EnemyBehaviour enemy = Container.Resolve<EnemyFactory>().Create(enemySpawnPoint, _charactersParent);
        _characterRegistry.RegisterEnemy(enemy);
      }

      await _battleStarter.WaitForBattleStart();
      _targetPicker.Initialize();
      InitializeHero(hero);
      foreach (EnemyBehaviour enemy in _characterRegistry.Enemies)
        enemy.Initialize(_characterRegistry, _gameplayPrefabProvider, _visibilityService, _transformContainer);
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

    private void InitializeHero(HeroBehaviour hero)
    {
      hero.Initialize(_heroSpawnPoint.Config, _inputService, _targetPicker);
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