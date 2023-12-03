using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public class EnemyFactory : SpawnPointFactory<EnemySpawnPoint, EnemyBehaviour>
  {
    private readonly TransformContainer _transformContainer;
    private readonly ICharacterRegistry _characterRegistry;

    public EnemyFactory(ICharacterRegistry characterRegistry, TransformContainer transformContainer)
    {
      _characterRegistry = characterRegistry;
      _transformContainer = transformContainer;
    }

    public override EnemyBehaviour Create(EnemySpawnPoint spawnPoint, Transform parent = null)
    {
      EnemyBehaviour result = base.Create(spawnPoint, parent);
      result.ApplyProperties(spawnPoint.Config, _characterRegistry, _transformContainer);
      return result;
    }

    protected override EnemyBehaviour GetPrefab(EnemySpawnPoint spawnPoint)
    {
      return spawnPoint.Config.Prefab;
    }
  }
}