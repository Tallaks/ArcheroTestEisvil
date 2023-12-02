using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero
{
  [RequireComponent(typeof(Collider))]
  public abstract class HeroProjectileBehaviourBase : MonoBehaviour
  {
    public int Damage { get; protected set; }
    public IEnumerable<IDamageApplier> DamageAppliers { get; protected set; }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out HitBox enemyHitBox) && enemyHitBox.Owner is EnemyBehaviour enemy)
        foreach (IDamageApplier damageApplier in DamageAppliers)
        {
          damageApplier.ApplyDamage(enemy.Health, Damage);
          PerformHit(transform.position);
        }
      else if (other.TryGetComponent(out ObstacleBehaviour obstacle))
        PerformHit(transform.position);
    }

    public abstract void PerformHit(Vector3 hitPosition);
  }
}