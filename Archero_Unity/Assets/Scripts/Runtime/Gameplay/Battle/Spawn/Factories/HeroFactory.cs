using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public class HeroFactory : SpawnPointFactory<HeroSpawnPoint, HeroBehaviour>
  {
    protected override HeroBehaviour GetPrefab(HeroSpawnPoint spawnPoint)
    {
      return spawnPoint.Config.Prefab;
    }
  }
}