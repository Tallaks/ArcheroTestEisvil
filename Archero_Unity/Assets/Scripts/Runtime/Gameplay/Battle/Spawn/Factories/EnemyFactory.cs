using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public class EnemyFactory : SpawnPointFactory<EnemySpawnPoint, EnemyBehaviour>
  {
    private readonly ICharacterRegistry _characterRegistry;
    private readonly IVisibilityService _visibilityService;

    public EnemyFactory(ICharacterRegistry characterRegistry, IVisibilityService visibilityService)
    {
      _characterRegistry = characterRegistry;
      _visibilityService = visibilityService;
    }

    public override EnemyBehaviour Create(EnemySpawnPoint spawnPoint, Transform parent = null)
    {
      EnemyBehaviour result = base.Create(spawnPoint, parent);
      result.Initialize(_characterRegistry, _visibilityService);
      return result;
    }

    protected override EnemyBehaviour GetPrefab(EnemySpawnPoint spawnPoint)
    {
      return spawnPoint.Config.Prefab;
    }
  }
}