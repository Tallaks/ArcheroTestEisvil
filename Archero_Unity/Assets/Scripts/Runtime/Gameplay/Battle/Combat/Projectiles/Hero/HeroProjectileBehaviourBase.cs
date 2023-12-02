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
          Debug.Log($"Applying damage to {enemy.name}");
          damageApplier.ApplyDamage(enemy.Health, Damage);
          PerformHit(enemy.Position);
        }
      else if (other.TryGetComponent(out ObstacleBehaviour obstacle))
        PerformHit(obstacle.transform.position);
    }

    public abstract void PerformHit(Vector3 hitPosition);
  }
}