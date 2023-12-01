using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Composites
{
  [Serializable]
  public class AllNode : CompositeNodeBase
  {
    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      for (var index = 0; index < ChildNodes.Length; index++)
      {
        NodeBase node = ChildNodes[index];
        if (node.GetResult(deltaTime, debug) == false)
        {
          if (debug)
            Debug.Log($"AllNode {Name}: \"{node.Name}\" node failed, returning false.");
          return false;
        }
      }

      return true;
    }
  }
}