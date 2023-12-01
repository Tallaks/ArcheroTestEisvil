using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Movement
{
  [Serializable]
  public abstract class MovementNodeBase : NodeBase
  {
    protected EnemyMovementBehaviourBase Movement;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      Movement = initializationParams.Owner.Movement;
    }
  }
}