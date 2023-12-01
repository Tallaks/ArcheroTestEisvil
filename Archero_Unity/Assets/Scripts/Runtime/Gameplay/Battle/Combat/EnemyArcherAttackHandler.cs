using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat
{
  public class EnemyArcherAttackHandler : EnemyAttackHandlerBase
  {
    public override void Initialize(EnemyBehaviour owner)
    {
      Cooldown = owner.Movement.MaxDistanceMovedByState / owner.Movement.Speed;
    }
  }
}