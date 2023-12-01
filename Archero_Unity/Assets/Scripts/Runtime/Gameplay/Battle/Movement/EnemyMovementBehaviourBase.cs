using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public abstract class EnemyMovementBehaviourBase : MonoBehaviour
  {
    [field: SerializeField] public float MaxDistanceMovedByState { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    public abstract void MoveTo(Vector3 position);
  }
}