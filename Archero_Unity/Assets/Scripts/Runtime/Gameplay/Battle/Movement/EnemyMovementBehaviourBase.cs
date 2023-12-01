using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public abstract class EnemyMovementBehaviourBase : MonoBehaviour
  {
    [field: SerializeField] public float MaxDistanceMovedByState { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    public Vector3 DeltaPosition { get; protected set; }
    private EnemyBehaviour _enemyBehaviour;

    public virtual void Initialize(EnemyBehaviour enemyBehaviour)
    {
      _enemyBehaviour = enemyBehaviour;
    }

    public abstract void MoveTo(Vector3 position);
    public abstract void Stop();

    public Vector3 GetPosition()
    {
      return _enemyBehaviour.Position;
    }
  }
}