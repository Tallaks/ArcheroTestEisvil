using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes
{
  [System.Serializable]
  public struct LevelProperties
  {
    [field: SerializeField] public int LevelIndex { get; private set; }

    public string GetSceneName()
    {
      return $"Level_{LevelIndex.ToString()}";
    }
  }
}