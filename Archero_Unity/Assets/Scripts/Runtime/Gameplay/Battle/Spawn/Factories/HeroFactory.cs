using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public class HeroFactory : SpawnPointFactory<HeroSpawnPoint, HeroBehaviour>
  {
    private readonly IInputService _inputService;

    public HeroFactory(IInputService inputService) =>
      _inputService = inputService;

    public override HeroBehaviour Create(HeroSpawnPoint spawnPoint, Transform parent = null)
    {
      HeroBehaviour hero = base.Create(spawnPoint, parent);
      hero.Initialize(_inputService);
      return hero;
    }

    protected override HeroBehaviour GetPrefab(HeroSpawnPoint spawnPoint)
    {
      return spawnPoint.Config.Prefab;
    }
  }
}