using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class ProjectInstaller : MonoInstaller
  {
    [SerializeField] private InputService _inputService;

    public override void InstallBindings()
    {
      Container
        .Bind<ISceneLoader>()
        .To<AsyncSceneLoader>()
        .AsSingle();

      Container
        .Bind<IInputService>()
        .FromInstance(_inputService)
        .AsSingle();

      Container
        .Bind<ICurtainService>()
        .To<CurtainService>()
        .AsSingle();
    }
  }
}