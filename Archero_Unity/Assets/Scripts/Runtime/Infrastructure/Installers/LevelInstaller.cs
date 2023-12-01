using System.Linq;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories;
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
    private IInputService _inputService;
    private ITargetPicker _targetPicker;

    [Inject]
    private void Construct(IInputService inputService, ITargetPicker targetPicker, ICharacterRegistry characterRegistry)
    {
      _inputService = inputService;
      _targetPicker = targetPicker;
      _characterRegistry = characterRegistry;
    }

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public void Initialize()
    {
      Debug.Log("Level initialized");
      HeroBehaviour hero = Container.Resolve<HeroFactory>().Create(_heroSpawnPoint, _charactersParent);
      _characterRegistry.RegisterHero(hero);

      EnemySpawnPoint[] enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
      foreach (EnemySpawnPoint enemySpawnPoint in enemySpawnPoints)
        Container.Resolve<EnemyFactory>().Create(enemySpawnPoint, _charactersParent);

      _targetPicker.Initialize();
      InitializeHero(hero);
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
        .Bind<IHeroAttackSystem>()
        .To<HeroAttackSystem>()
        .AsSingle();
    }

    private void InitializeHero(HeroBehaviour hero)
    {
      hero.Initialize(_heroSpawnPoint.Config, _inputService, _targetPicker);
      IHeroAttackHandler heroDefaultAttackDirection =
        Container.Resolve<HeroAttackHandlerFactory>().Create(_heroSpawnPoint.Config, hero);

      var heroAttackSystem = Container.Resolve<IHeroAttackSystem>();
      heroAttackSystem.AddAttackHandler(heroDefaultAttackDirection);
      heroAttackSystem.StartWorking();
    }
  }
}