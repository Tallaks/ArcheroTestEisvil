using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Decorators
{
  [Serializable]
  public class RootNode : DecoratorNodeBase
  {
    public override bool GetResult(float deltaTime, bool debug = false)
    {
      if (debug)
        Debug.Log("*********************************");
      base.GetResult(deltaTime, debug);
      return ChildNode.GetResult(deltaTime, debug);
    }
  }
}