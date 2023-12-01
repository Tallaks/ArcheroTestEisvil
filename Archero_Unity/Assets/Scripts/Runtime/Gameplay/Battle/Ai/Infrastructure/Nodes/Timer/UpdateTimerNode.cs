using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer
{
  [Serializable]
  public class UpdateTimerNode : TimerNodeBase
  {
    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      Timer.UpdateTime(deltaTime);
      return true;
    }
  }
}