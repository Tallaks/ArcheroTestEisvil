using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn
{
  public class EnemySpawnPoint : SpawnPointBase
  {
    [SerializeField] public EnemyConfig Config;
  }
}