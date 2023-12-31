using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies
{
  public class EnemyArrowBehaviour : EnemyProjectileBehaviourBase
  {
    private readonly WaitUntil _waitFrameUnpaused = new(() => _isPaused == false);
    private static bool _isPaused;

    [SerializeField] private float _speed;
    private EnemyBehaviour _owner;
    private EnemyArrowPool _pool;

    private Coroutine _shootRoutine;
    private IVisualEffectPerformer _visualEffectPerformer;

    public void Reinitialize(EnemyBehaviour owner, EnemyArrowPool pool, IVisualEffectPerformer visualEffectPerformer)
    {
      _owner = owner;
      _pool = pool;
      _visualEffectPerformer = visualEffectPerformer;
      Damage = _owner.BaseDamage;
      DamageApplier = new ValueDamageApplier();
    }

    public void ShootAt(Vector3 targetPosition)
    {
      _shootRoutine = StartCoroutine(ShootRoutine(targetPosition));
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

    public override void PerformHit(Vector3 hitPosition)
    {
      _pool.Release(this);
      _visualEffectPerformer.Play(ParticleType.DefaultProjectileHit, hitPosition);
    }

    public override void OnResume()
    {
      base.OnResume();
      _isPaused = false;
    }

    public override void OnPause()
    {
      base.OnPause();
      _isPaused = true;
    }

    private IEnumerator ShootRoutine(Vector3 targetPosition)
    {
      transform.LookAt(targetPosition);
      while (true)
      {
        transform.position += transform.forward * (_speed * Time.deltaTime);
        yield return _waitFrameUnpaused;
      }
    }
  }
}