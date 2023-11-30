using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public class HeroFactory : SpawnPointFactory<HeroSpawnPoint, HeroBehaviour>
  {
    public override HeroBehaviour Create(HeroSpawnPoint spawnPoint, Transform parent = null)
    {
      HeroBehaviour hero = base.Create(spawnPoint, parent);
      return hero;
    }

    protected override HeroBehaviour GetPrefab(HeroSpawnPoint spawnPoint)
    {
      return spawnPoint.Config.Prefab;
    }
  }
}