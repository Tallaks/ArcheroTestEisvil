using System.Collections;
using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero
{
  [RequireComponent(typeof(Collider))]
  public abstract class HeroProjectileBehaviourBase : MonoBehaviour, IPauseHandler
  {
    public int Damage { get; protected set; }
    public IEnumerable<IDamageApplier> DamageAppliers { get; protected set; }

    private IEnumerator OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out HitBox enemyHitBox) && enemyHitBox.Owner is EnemyBehaviour
          {
            IsDead: false
          } enemy)
      {
        foreach (IDamageApplier damageApplier in DamageAppliers)
        {
          damageApplier.ApplyDamage(enemy.Health, Damage);
          yield return null;
          PerformHit(transform.position);
        }
      }
      else if (other.TryGetComponent(out ObstacleBehaviour obstacle))
      {
        yield return null;
        PerformHit(transform.position);
      }
    }

    public virtual void OnPause()
    {
      enabled = false;
    }

    public virtual void OnResume()
    {
      enabled = true;
    }

    public abstract void PerformHit(Vector3 hitPosition);
  }
}