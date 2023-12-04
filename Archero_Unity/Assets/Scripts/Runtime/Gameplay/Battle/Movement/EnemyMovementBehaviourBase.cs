using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public abstract class EnemyMovementBehaviourBase : MonoBehaviour, IDisposable
  {
    public float MaxDistanceMovedByState { get; private set; }
    public float Speed { get; protected set; }

    protected EnemyBehaviour Owner;

    public virtual void Initialize(EnemyBehaviour enemyBehaviour)
    {
      Owner = enemyBehaviour;
      MaxDistanceMovedByState = Owner.MaxDistanceMovedByState;
      Speed = Owner.Speed;
    }

    public abstract void Dispose();
    public abstract void MoveTo(Vector3 position);
    public abstract void Stop();

    public void LookAt(Vector3 heroPosition)
    {
      transform.LookAt(heroPosition);
    }
  }
}