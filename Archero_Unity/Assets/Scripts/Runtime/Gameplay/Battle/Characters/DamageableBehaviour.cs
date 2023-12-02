using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public abstract class DamageableBehaviour : MonoBehaviour, IDamageable
  {
    public Health Health { get; protected set; }
    public bool IsDead => Health.Current <= 0;
    public abstract void Die();
  }
}