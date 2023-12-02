using System;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers
{
  public interface IGameplayPrefabProvider : IDisposable
  {
    UniTask LoadHeroProjectilesAsync();
    UniTask LoadEnemyProjectilesAsync();
    T GetHeroProjectilePrefab<T>() where T : HeroProjectileBehaviourBase;
    T GetEnemyProjectilePrefab<T>() where T : EnemyProjectileBehaviourBase;
  }
}