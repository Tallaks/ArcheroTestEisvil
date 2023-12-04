using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class FlyingEnemyMovementBehaviour : EnemyMovementBehaviourBase
  {
    [SerializeField] private CharacterController _characterController;
    private Coroutine _movingRoutine;

    public override void MoveTo(Vector3 position)
    {
      Vector3 direction = (position - transform.position).normalized;
      _characterController.Move(direction * (Speed * Time.deltaTime));
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