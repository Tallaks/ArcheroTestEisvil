using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public abstract class SpawnPointFactory<TSpawnPoint, TObject>
    where TObject : Object
    where TSpawnPoint : SpawnPointBase
  {
    public virtual TObject Create(TSpawnPoint spawnPoint, Transform parent = null)
    {
      TObject result = Object.Instantiate(GetPrefab(spawnPoint), spawnPoint.Position, spawnPoint.Rotation, parent);
      return result;
    }

    protected abstract TObject GetPrefab(TSpawnPoint spawnPoint);
  }
}