using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero;
using UnityEngine.Pool;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools
{
  public class HeroArrowPool : ObjectPool<ArrowBehaviour>
  {
    public HeroArrowPool(Func<ArrowBehaviour> createFunc, Action<ArrowBehaviour> actionOnGet = null,
      Action<ArrowBehaviour> actionOnRelease = null, Action<ArrowBehaviour> actionOnDestroy = null,
      bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 100) : base(createFunc, actionOnGet,
      actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
    {
    }
  }
}