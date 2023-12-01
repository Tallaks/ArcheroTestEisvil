using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Decorators
{
  [Serializable]
  public abstract class DecoratorNodeBase : NodeBase
  {
    [SerializeReference] [SubclassSelector] public NodeBase ChildNode;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      ChildNode.Initialize(initializationParams);
    }
  }
}