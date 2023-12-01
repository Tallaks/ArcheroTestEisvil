using System;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers
{
  public interface IGameplayPrefabProvider : IDisposable
  {
    UniTask LoadHeroProjectiles();
    T GetHeroProjectilePrefab<T>() where T : HeroProjectileBehaviourBase;
  }
}