using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn
{
  public class HeroSpawnPoint : SpawnPointBase
  {
    [field: SerializeField] public HeroConfig Config { get; private set; }
  }
}