using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers
{
  public interface IConfigProvider<T> where T : ScriptableObject
  {
    T Get(string id);
    void LoadAll();
    void UnloadAll();
  }
}