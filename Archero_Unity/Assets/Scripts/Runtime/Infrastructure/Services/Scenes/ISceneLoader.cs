using System;
using UnityEngine.SceneManagement;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes
{
  public interface ISceneLoader
  {
    void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, Action onLoaded = null);
    void UnloadScene(string sceneName, Action onUnloaded = null);
  }
}