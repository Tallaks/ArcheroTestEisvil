using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class NavMeshMovementBehaviour : EnemyMovementBehaviourBase
  {
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Update()
    {
      Owner.Position = transform.position;
      transform.localPosition = Vector3.zero;
    }

    public override void Initialize(EnemyBehaviour enemyBehaviour)
    {
      base.Initialize(enemyBehaviour);
      _navMeshAgent.speed = Speed;
      _navMeshAgent.updateRotation = false;
    }

    public override void MoveTo(Vector3 position)
    {
      if (_navMeshAgent.isStopped)
        _navMeshAgent.isStopped = false;
      _navMeshAgent.SetDestination(position);
    }

    public override void Dispose()
    {
      enabled = false;
      _navMeshAgent.enabled = false;
    }

    public override void OnPause()
    {
      _navMeshAgent.isStopped = true;
      base.OnPause();
    }

    public override void OnResume()
    {
      base.OnResume();
      _navMeshAgent.isStopped = false;
    }

    public override void Stop()
    {
      _navMeshAgent.isStopped = true;
    }
  }
}