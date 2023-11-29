using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes
{
  public class AsyncSceneLoader : ISceneLoader
  {
    public async void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single,
      Action onLoaded = null)
    {
      await SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
      onLoaded?.Invoke();
    }

    public async void UnloadScene(string sceneName, Action onUnloaded = null)
    {
      await SceneManager.UnloadSceneAsync(sceneName);
      onUnloaded?.Invoke();
    }
  }
}