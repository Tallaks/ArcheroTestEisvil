using System.Collections.Generic;

namespace Zenject
{
  public class ArrayPool<T> : StaticMemoryPoolBaseBase<T[]>
  {
    private readonly int _length;

    private static readonly Dictionary<int, ArrayPool<T>> _pools = new();

    public ArrayPool(int length)
      : base(OnDespawned) =>
      _length = length;

    public T[] Spawn()
    {
#if ZEN_MULTITHREADING
            lock (_locker)
#endif
      {
        return SpawnInternal();
      }
    }

    public static ArrayPool<T> GetPool(int length)
    {
      ArrayPool<T> pool;

      if (!_pools.TryGetValue(length, out pool))
      {
        pool = new ArrayPool<T>(length);
        _pools.Add(length, pool);
      }

      return pool;
    }

    protected override T[] Alloc()
    {
      return new T[_length];
    }

    private static void OnDespawned(T[] arr)
    {
      for (var i = 0; i < arr.Length; i++)
        arr[i] = default;
    }
  }
}