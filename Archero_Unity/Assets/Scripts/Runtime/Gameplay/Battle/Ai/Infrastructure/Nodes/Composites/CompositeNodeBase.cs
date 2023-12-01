using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Composites
{
  [Serializable]
  public abstract class CompositeNodeBase : NodeBase
  {
    [SerializeReference] [SubclassSelector] public NodeBase[] ChildNodes;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      foreach (NodeBase childNode in ChildNodes)
        childNode.Initialize(initializationParams);
    }
  }
}