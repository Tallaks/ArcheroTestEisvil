using System.Linq;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    private ICurtainService _curtainService;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader, ICurtainService curtainService)
    {
      _sceneLoader = sceneLoader;
      _curtainService = curtainService;
    }

#if UNITY_EDITOR
    private async void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public async void Initialize()
    {
      await _curtainService.InitializeAsync();
      _curtainService.Show();
      _sceneLoader.LoadScene(SceneNames.Gameplay);
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BootstrapInstaller>()
        .FromInstance(this)
        .AsSingle();
    }
  }
}