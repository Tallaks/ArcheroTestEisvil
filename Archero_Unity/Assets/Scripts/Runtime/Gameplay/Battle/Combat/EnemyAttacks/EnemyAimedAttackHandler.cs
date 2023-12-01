using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Aiming;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public abstract class EnemyAimedAttackHandler : EnemyAttackHandlerBase
  {
    [field: SerializeField] public AimingDrawerBehaviourBase AimingDrawer { get; private set; }
    [field: SerializeField] public float AimDurationSec { get; private set; }

    public override void Initialize(EnemyBehaviour owner)
    {
      AimingDrawer.Initialize(owner);
    }
  }
}