using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies;
using UnityEngine.Pool;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools
{
  public class EnemyArrowPool : ObjectPool<EnemyArrowBehaviour>
  {
    public EnemyArrowPool(Func<EnemyArrowBehaviour> createFunc, Action<EnemyArrowBehaviour> actionOnGet = null,
      Action<EnemyArrowBehaviour> actionOnRelease = null, Action<EnemyArrowBehaviour> actionOnDestroy = null,
      bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 100) : base(createFunc, actionOnGet,
      actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
    {
    }
  }
}