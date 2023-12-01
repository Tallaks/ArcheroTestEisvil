using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Decorators
{
  [Serializable]
  public class NotNode : DecoratorNodeBase
  {
    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      return ChildNode.GetResult(deltaTime, debug) == false;
    }
  }
}