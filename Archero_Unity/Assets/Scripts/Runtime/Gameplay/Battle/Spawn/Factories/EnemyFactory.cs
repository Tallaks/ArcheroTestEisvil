using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public class EnemyFactory : SpawnPointFactory<EnemySpawnPoint, EnemyBehaviour>
  {
    public override EnemyBehaviour Create(EnemySpawnPoint spawnPoint, Transform parent = null)
    {
      EnemyBehaviour result = base.Create(spawnPoint, parent);
      return result;
    }

    protected override EnemyBehaviour GetPrefab(EnemySpawnPoint spawnPoint)
    {
      return spawnPoint.Config.Prefab;
    }
  }
}