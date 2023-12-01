using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer
{
  [Serializable]
  public class TimerIsRunningNode : TimerNodeBase
  {
    public override bool GetResult(float deltaTime)
    {
      return Timer.IsRunning;
    }
  }
}