using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class FlyingEnemyMovementBehaviour : EnemyMovementBehaviourBase
  {
    private Coroutine _movingRoutine;

    public override void MoveTo(Vector3 position)
    {
      Vector3 direction = (position - transform.position).normalized;
      Owner.Position += direction * Owner.Speed * Time.deltaTime;
    }

    public override void Stop()
    {
      if (_movingRoutine != null)
        StopCoroutine(_movingRoutine);
    }

    public override void Dispose()
    {
      _movingRoutine = null;
    }
  }
}