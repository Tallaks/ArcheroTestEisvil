using System.Collections.Generic;

namespace Zenject
{
  public class ListPool<T> : StaticMemoryPool<List<T>>
  {
    public static ListPool<T> Instance { get; } = new();

    public ListPool() =>
      OnDespawnedMethod = OnDespawned;

    private void OnDespawned(List<T> list)
    {
      list.Clear();
    }
  }
}