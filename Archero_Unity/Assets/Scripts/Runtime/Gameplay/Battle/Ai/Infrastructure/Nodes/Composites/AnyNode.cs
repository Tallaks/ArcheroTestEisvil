using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Composites
{
  [Serializable]
  public class AnyNode : CompositeNodeBase
  {
    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      for (var index = 0; index < ChildNodes.Length; index++)
      {
        NodeBase node = ChildNodes[index];
        if (node.GetResult(deltaTime, debug))
        {
          if (debug)
            Debug.Log($"AnyNode {Name}: \"{node.Name}\" node succeeded, returning true.");
          return true;
        }
      }

      return false;
    }
  }
}