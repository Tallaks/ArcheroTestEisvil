using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers
{
  public class GameplayPrefabProvider : IGameplayPrefabProvider
  {
    private readonly string[] _heroProjectileIds = { "Hero", "Projectile", "Gameplay" };
    private readonly string[] _enemyProjectileIds = { "Enemy", "Projectile", "Gameplay" };

    private Dictionary<Type, HeroProjectileBehaviourBase> _heroProjectilePrefabsByType = new();
    private Dictionary<Type, EnemyProjectileBehaviourBase> _enemyProjectilePrefabsByType = new();

    public async UniTask LoadHeroProjectilesAsync()
    {
      IList<GameObject> allHeroProjectiles =
        await Addressables
          .LoadAssetsAsync<GameObject>(_heroProjectileIds as IEnumerable, null, Addressables.MergeMode.Intersection)
          .Task;
      _heroProjectilePrefabsByType = allHeroProjectiles
        .Select(k => k.GetComponent<HeroProjectileBehaviourBase>())
        .ToDictionary(heroProjectile => heroProjectile.GetType());
    }

    public async UniTask LoadEnemyProjectilesAsync()
    {
      IList<GameObject> allEnemyProjectiles =
        await Addressables
          .LoadAssetsAsync<GameObject>(_enemyProjectileIds as IEnumerable, null, Addressables.MergeMode.Intersection)
          .Task;
      _enemyProjectilePrefabsByType = allEnemyProjectiles
        .Select(k => k.GetComponent<EnemyProjectileBehaviourBase>())
        .ToDictionary(heroProjectile => heroProjectile.GetType());
    }

    public T GetHeroProjectilePrefab<T>() where T : HeroProjectileBehaviourBase
    {
      return _heroProjectilePrefabsByType[typeof(T)] as T;
    }

    public T GetEnemyProjectilePrefab<T>() where T : EnemyProjectileBehaviourBase
    {
      return _enemyProjectilePrefabsByType[typeof(T)] as T;
    }

    public void Dispose()
    {
      Addressables.Release(_heroProjectilePrefabsByType.Values);
    }
  }
}