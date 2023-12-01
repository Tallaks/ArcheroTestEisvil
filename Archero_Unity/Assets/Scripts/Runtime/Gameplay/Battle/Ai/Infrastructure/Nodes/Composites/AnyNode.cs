using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Composites
{
  [Serializable]
  public class AnyNode : CompositeNodeBase
  {
    public override bool GetResult(float deltaTime)
    {
      for (var index = 0; index < ChildNodes.Length; index++)
      {
        NodeBase node = ChildNodes[index];
        if (node.GetResult(deltaTime))
          return true;
      }

      return false;
    }
  }
}