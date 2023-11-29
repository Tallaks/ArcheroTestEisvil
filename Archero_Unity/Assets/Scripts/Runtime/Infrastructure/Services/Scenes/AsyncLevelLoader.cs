using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes
{
  public class AsyncLevelLoader : IAsyncLevelLoader
  {
    public AsyncOperationHandle<SceneInstance> LoadLevel(LevelProperties levelProperties)
    {
      return Addressables.LoadSceneAsync(levelProperties.GetSceneName(), LoadSceneMode.Additive);
    }

    public AsyncOperationHandle<SceneInstance> UnloadLevel(SceneInstance levelSceneInstance)
    {
      return Addressables.UnloadSceneAsync(levelSceneInstance);
    }
  }
}