using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public abstract class EnemyMovementBehaviourBase : MonoBehaviour, IDisposable
  {
    [field: SerializeField] public float MaxDistanceMovedByState { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    public Vector3 DeltaPosition { get; protected set; }
    protected EnemyBehaviour Owner;

    public virtual void Initialize(EnemyBehaviour enemyBehaviour)
    {
      Owner = enemyBehaviour;
    }

    public abstract void Dispose();

    public abstract void MoveTo(Vector3 position);
    public abstract void Stop();

    public Vector3 GetPosition()
    {
      return Owner.Position;
    }
  }
}