using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private LevelProperties _firstLevelProperties;

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
    }
  }
}