using UnityEngine;
using UnityEngine.AI;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class NavMeshMovementBehaviour : EnemyMovementBehaviourBase
  {
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public override void MoveTo(Vector3 position)
    {
      if (_navMeshAgent.isStopped)
        _navMeshAgent.isStopped = false;
      _navMeshAgent.SetDestination(position);
    }

    public override void Stop()
    {
      _navMeshAgent.isStopped = true;
    }
  }
}