using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public abstract class EnemyMovementBehaviourBase : MonoBehaviour, IPauseHandler, IDisposable
  {
    public float MaxDistanceMovedByState { get; private set; }
    public float Speed { get; protected set; }

    protected EnemyBehaviour Owner;

    public virtual void Initialize(EnemyBehaviour enemyBehaviour)
    {
      enabled = true;
      Owner = enemyBehaviour;
      MaxDistanceMovedByState = Owner.MaxDistanceMovedByState;
      Speed = Owner.Speed;
    }

    public virtual void Dispose()
    {
    }

    public virtual void OnPause()
    {
    }

    public virtual void OnResume()
    {
    }

    public void LookAt(Vector3 heroPosition)
    {
      transform.LookAt(heroPosition);
    }

    public abstract void MoveTo(Vector3 position);

    public virtual void Stop()
    {
    }
  }
}