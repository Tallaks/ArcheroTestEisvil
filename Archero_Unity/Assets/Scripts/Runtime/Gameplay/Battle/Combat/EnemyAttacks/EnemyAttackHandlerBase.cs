using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public abstract class EnemyAttackHandlerBase : MonoBehaviour
  {
    public float Cooldown { get; protected set; }
    public abstract void Initialize(EnemyBehaviour owner);
    public abstract void Attack(Vector3 heroPosition);
  }
}