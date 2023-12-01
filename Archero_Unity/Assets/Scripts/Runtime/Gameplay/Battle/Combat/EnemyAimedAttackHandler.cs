using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Aiming;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat
{
  public abstract class EnemyAimedAttackHandler : EnemyAttackHandlerBase
  {
    [field: SerializeField] public AimingDrawerBehaviourBase AimingDrawer { get; private set; }

    public override void Initialize(EnemyBehaviour owner)
    {
      AimingDrawer.Initialize(owner);
    }
  }
}