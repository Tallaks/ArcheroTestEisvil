using System.Collections;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class FlyingEnemyMovementBehaviour : EnemyMovementBehaviourBase
  {
    private Coroutine _movingRoutine;

    public override void MoveTo(Vector3 position)
    {
      _movingRoutine = StartCoroutine(MoveToRoutine(position));
    }

    private IEnumerator MoveToRoutine(Vector3 position)
    {
      while (Vector3.Distance(transform.position, position) > 0.01f)
      {
        Owner.Position = Vector3.MoveTowards(transform.position, position, Speed * Time.deltaTime);
        yield return null;
      }
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