using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class DamageableBehaviour : MonoBehaviour, IDamageable
  {
    public Health Health { get; protected set; }
  }
}