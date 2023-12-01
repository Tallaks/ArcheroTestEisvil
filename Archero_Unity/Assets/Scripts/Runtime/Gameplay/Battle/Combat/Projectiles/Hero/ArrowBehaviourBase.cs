using System;
using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero
{
  public class ArrowBehaviourBase : HeroProjectileBehaviourBase
  {
    [SerializeField] private float _speed;

    private HeroBehaviour _owner;
    private ArrowPool _pool;
    private Coroutine _shootRoutine;

    public void Initialize(HeroBehaviour owner, ArrowPool pool)
    {
      _owner = owner;
      _pool = pool;
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
    }

    public void GetFromPool()
    {
      transform.position = _owner.transform.position.WithY(PhysicsConstants.ProjectileHeight);
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