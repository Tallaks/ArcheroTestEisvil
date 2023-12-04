using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies
{
  public abstract class EnemyProjectileBehaviourBase : MonoBehaviour, IPauseHandler
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