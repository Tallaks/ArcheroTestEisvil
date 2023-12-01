using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer
{
  [Serializable]
  public class ResetTimerNode : TimerNodeBase
  {
    public override bool GetResult(float deltaTime)
    {
      Timer.Reset();
      return true;
    }
  }
}