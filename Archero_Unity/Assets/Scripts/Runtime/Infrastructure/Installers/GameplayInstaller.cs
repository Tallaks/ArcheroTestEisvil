using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using Tallaks.ArcheroTest.Runtime.UI.Gameplay;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private LevelProperties _firstLevelProperties;
    [SerializeField] private TransformContainer _transformContainer;
    [SerializeField] private GameplayUi _gameplayUi;
    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService inputService)
    {
      _inputService = inputService;
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
      InitializeAsync();
    }

    private async UniTaskVoid InitializeAsync()
    {
      await Container.Resolve<IGameplayPrefabProvider>().LoadHeroProjectiles();
      await Container.Resolve<IGameplayPrefabProvider>().LoadEnemyProjectiles();
      await Container.Resolve<IAsyncLevelLoader>().LoadLevel(_firstLevelProperties);
      _gameplayUi.Initialize(_inputService, Container.Resolve<IBattleStarter>());
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<GameplayInstaller>()
        .FromInstance(this)
        .AsTransient();

      Container
        .Bind<IAsyncLevelLoader>()
        .To<AsyncLevelLoader>()
        .AsSingle();

      Container
        .Bind<ICharacterRegistry>()
        .To<CharacterRegistry>()
        .AsSingle();

      Container
        .Bind<IVisibilityService>()
        .To<VisibilityService>()
        .AsSingle();

      Container
        .Bind<IGameplayPrefabProvider>()
        .To<GameplayPrefabProvider>()
        .AsSingle();

      Container
        .Bind<ITargetPicker>()
        .To<HeroTargetPicker>()
        .AsSingle();

      Container
        .Bind<TransformContainer>()
        .FromInstance(_transformContainer)
        .AsSingle();

      Container
        .Bind<IBattleStarter>()
        .To<BattleStarter>()
        .AsSingle();
    }
  }
}