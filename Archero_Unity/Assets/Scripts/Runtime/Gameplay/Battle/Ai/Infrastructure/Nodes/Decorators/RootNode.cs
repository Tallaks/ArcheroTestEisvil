using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Decorators
{
  [Serializable]
  public class RootNode : DecoratorNodeBase
  {
    public override bool GetResult(float deltaTime)
    {
      return ChildNode.GetResult(deltaTime);
    }
  }
}