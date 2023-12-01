using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer
{
  [Serializable]
  public class StartTimerNode : TimerNodeBase
  {
    public override bool GetResult(float deltaTime)
    {
      Timer.StartRunning();
      return true;
    }
  }
}