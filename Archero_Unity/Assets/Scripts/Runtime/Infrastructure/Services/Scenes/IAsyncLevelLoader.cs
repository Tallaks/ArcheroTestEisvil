using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes
{
  public interface IAsyncLevelLoader
  {
    AsyncOperationHandle<SceneInstance> LoadLevelAsync(LevelProperties levelProperties);
    AsyncOperationHandle<SceneInstance> UnloadLevel(SceneInstance levelSceneInstance);
  }
}