using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
  public class DictionaryPool<TKey, TValue> : StaticMemoryPool<Dictionary<TKey, TValue>>
  {
    public static DictionaryPool<TKey, TValue> Instance { get; } = new();

    public DictionaryPool()
    {
      OnSpawnMethod = OnSpawned;
      OnDespawnedMethod = OnDespawned;
    }

    private static void OnSpawned(Dictionary<TKey, TValue> items)
    {
      Assert.That(items.IsEmpty());
    }

    private static void OnDespawned(Dictionary<TKey, TValue> items)
    {
      items.Clear();
    }
  }
}