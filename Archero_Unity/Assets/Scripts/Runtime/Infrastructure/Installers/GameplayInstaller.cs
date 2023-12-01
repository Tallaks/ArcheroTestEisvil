using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
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

    public async void Initialize()
    {
      await Container.Resolve<IAsyncLevelLoader>().LoadLevel(_firstLevelProperties);
      _gameplayUi.Initialize(_inputService);
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
    }
  }
}