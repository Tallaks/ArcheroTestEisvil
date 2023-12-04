using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
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
    private ICurtainService _curtainService;
    private IInputService _inputService;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(IInputService inputService, ISceneLoader sceneLoader, ICurtainService curtainService)
    {
      _inputService = inputService;
      _sceneLoader = sceneLoader;
      _curtainService = curtainService;
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
        .Bind<IGameplayPrefabProvider>()
        .To<GameplayPrefabProvider>()
        .AsSingle();

      Container
        .Bind<TransformContainer>()
        .FromInstance(_transformContainer)
        .AsSingle();

      Container
        .Bind<IBattleStarter>()
        .To<BattleStarter>()
        .AsSingle();

      Container
        .Bind<IParticlePrefabProvider>()
        .To<ParticlePrefabProvider>()
        .AsSingle();

      Container
        .Bind<IVisualEffectPerformer>()
        .To<VisualEffectPerformer>()
        .AsSingle();

      Container
        .Bind<IPauseService>()
        .To<PauseService>()
        .AsSingle();
    }

    private async UniTaskVoid InitializeAsync()
    {
      Container.Resolve<IVisualEffectPerformer>().Initialize();
      await Container.Resolve<IParticlePrefabProvider>().LoadParticlePrefabsAsync();
      await Container.Resolve<IGameplayPrefabProvider>().LoadHeroProjectilesAsync();
      await Container.Resolve<IGameplayPrefabProvider>().LoadEnemyProjectilesAsync();
      await Container.Resolve<IAsyncLevelLoader>().LoadLevelAsync(_firstLevelProperties);
      var pauseService = Container.Resolve<IPauseService>();
      var battleStarter = Container.Resolve<IBattleStarter>();
      _gameplayUi.Initialize(_inputService, _sceneLoader, _curtainService, pauseService, battleStarter);
    }
  }
}