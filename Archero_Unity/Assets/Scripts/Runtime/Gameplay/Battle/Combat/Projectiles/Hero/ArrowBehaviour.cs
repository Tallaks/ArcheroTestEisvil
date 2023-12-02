using System;
using System.Collections;
using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero
{
  public class ArrowBehaviour : HeroProjectileBehaviourBase
  {
    [SerializeField] private float _speed;

    private IHeroAttackSystem _attackSystem;

    private HeroBehaviour _owner;
    private HeroArrowPool _pool;
    private Coroutine _shootRoutine;

    public void Initialize(HeroBehaviour owner, HeroArrowPool pool, IHeroAttackSystem attackSystem)
    {
      _owner = owner;
      _pool = pool;
      Damage = _owner.BaseDamage;
      DamageAppliers = new List<IDamageApplier>(attackSystem.DamageAppliers);
    }

    public void ShootAt(Vector3 targetPosition, HeroConfig.DefaultAttackDirection defaultAttackDirection)
    {
      _shootRoutine = StartCoroutine(ShootRoutine(targetPosition, defaultAttackDirection));
    }

    public void ReturnToPool()
    {
      if (_shootRoutine != null)
        StopCoroutine(_shootRoutine);
      transform.position = Vector3.down;
      DamageAppliers = null;
    }

    public void GetFromPool()
    {
      transform.position = _owner.transform.position.WithY(PhysicsConstants.ProjectileHeight);
    }

    public override void PerformHit(Vector3 hitPosition)
    {
      _pool.Release(this);
    }

    private IEnumerator ShootRoutine(Vector3 targetPosition, HeroConfig.DefaultAttackDirection defaultAttackDirection)
    {
      transform.LookAt(GetDirection(targetPosition, defaultAttackDirection));
      while (true)
      {
        transform.position += transform.forward * (_speed * Time.deltaTime);
        yield return null;
      }
    }

    private static Vector3 GetDirection(Vector3 attackDirection,
      HeroConfig.DefaultAttackDirection defaultAttackDirection)
    {
      switch (defaultAttackDirection)
      {
        case HeroConfig.DefaultAttackDirection.None:
          break;
        case HeroConfig.DefaultAttackDirection.Forward:
          return attackDirection;
        default:
          throw new ArgumentOutOfRangeException(nameof(defaultAttackDirection), defaultAttackDirection, null);
      }

      throw new ArgumentOutOfRangeException(nameof(defaultAttackDirection), defaultAttackDirection, null);
    }
  }
}