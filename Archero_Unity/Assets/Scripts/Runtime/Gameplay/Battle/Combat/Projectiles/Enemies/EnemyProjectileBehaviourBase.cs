using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies
{
  public abstract class EnemyProjectileBehaviourBase : MonoBehaviour
  {
    public int Damage { get; protected set; }
    public ValueDamageApplier DamageApplier { get; protected set; }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out HitBox enemyHitBox) && enemyHitBox.Owner is HeroBehaviour hero)
      {
        DamageApplier.ApplyDamage(hero.Health, Damage);
        PerformHit(transform.position);
      }
      else if (other.TryGetComponent(out ObstacleBehaviour obstacle))
      {
        PerformHit(transform.position);
      }
    }

    public abstract void PerformHit(Vector3 hitPosition);
  }
}