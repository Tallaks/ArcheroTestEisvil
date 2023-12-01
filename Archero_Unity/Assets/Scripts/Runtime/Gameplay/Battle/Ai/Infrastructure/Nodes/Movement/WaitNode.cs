using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Movement
{
  [Serializable]
  public class WaitNode : MovementNodeBase
  {
    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      Movement.Stop();
      return true;
    }
  }
}