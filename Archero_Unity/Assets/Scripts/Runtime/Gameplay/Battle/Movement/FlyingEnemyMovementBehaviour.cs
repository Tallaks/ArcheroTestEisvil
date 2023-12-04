using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class FlyingEnemyMovementBehaviour : EnemyMovementBehaviourBase
  {
    public override void MoveTo(Vector3 position)
    {
      Vector3 direction = (position - transform.position).normalized;
      Owner.Position += direction * (Owner.Speed * Time.deltaTime);
    }
  }
}