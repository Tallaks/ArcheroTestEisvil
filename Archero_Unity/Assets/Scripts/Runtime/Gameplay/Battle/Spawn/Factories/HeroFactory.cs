using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories
{
  public class HeroFactory : SpawnPointFactory<HeroSpawnPoint, HeroBehaviour>
  {
    private readonly IInputService _inputService;
    private readonly ICharacterRegistry _characterRegistry;

    public HeroFactory(IInputService inputService, ICharacterRegistry characterRegistry)
    {
      _inputService = inputService;
      _characterRegistry = characterRegistry;
    }

    public override HeroBehaviour Create(HeroSpawnPoint spawnPoint, Transform parent = null)
    {
      HeroBehaviour hero = base.Create(spawnPoint, parent);
      hero.Initialize(spawnPoint.Config, _inputService, _characterRegistry);
      return hero;
    }

    protected override HeroBehaviour GetPrefab(HeroSpawnPoint spawnPoint)
    {
      return spawnPoint.Config.Prefab;
    }
  }
}