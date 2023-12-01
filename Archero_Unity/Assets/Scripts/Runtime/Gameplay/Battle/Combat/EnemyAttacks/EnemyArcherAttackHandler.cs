using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public class EnemyArcherAttackHandler : EnemyAimedAttackHandler
  {
    public override void Initialize(EnemyBehaviour owner)
    {
      base.Initialize(owner);
      Cooldown = owner.Movement.MaxDistanceMovedByState / owner.Movement.Speed;
      Debug.Log($"Enemy archer attack cooldown: {Cooldown}");
    }

    public override void Attack(Vector3 heroPosition)
    {
    }
  }
}