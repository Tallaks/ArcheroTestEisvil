using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
#if UNITY_EDITOR
  public static class StaticMemoryPoolRegistry
  {
    private static readonly List<IMemoryPool> _pools = new();

    public static event Action<IMemoryPool> PoolAdded = delegate
    {
    };

    public static event Action<IMemoryPool> PoolRemoved = delegate
    {
    };

    public static IEnumerable<IMemoryPool> Pools => _pools;

    public static void Add(IMemoryPool memoryPool)
    {
      _pools.Add(memoryPool);
      PoolAdded(memoryPool);
    }

    public static void Remove(IMemoryPool memoryPool)
    {
      _pools.RemoveWithConfirm(memoryPool);
      PoolRemoved(memoryPool);
    }
  }
#endif
}