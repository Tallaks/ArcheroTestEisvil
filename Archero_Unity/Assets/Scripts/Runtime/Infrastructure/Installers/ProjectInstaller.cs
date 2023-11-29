using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class ProjectInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container
        .Bind<ISceneLoader>()
        .To<AsyncSceneLoader>()
        .AsSingle();
    }
  }
}