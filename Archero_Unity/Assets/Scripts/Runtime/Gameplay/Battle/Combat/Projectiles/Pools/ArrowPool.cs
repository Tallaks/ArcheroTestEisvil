using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero;
using UnityEngine.Pool;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools
{
  public class ArrowPool : ObjectPool<ArrowBehaviourBase>
  {
    public ArrowPool(Func<ArrowBehaviourBase> createFunc, Action<ArrowBehaviourBase> actionOnGet = null,
      Action<ArrowBehaviourBase> actionOnRelease = null, Action<ArrowBehaviourBase> actionOnDestroy = null,
      bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 100) : base(createFunc, actionOnGet,
      actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
    {
    }
  }
}