using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer
{
  [Serializable]
  public class UpdateTimerNode : TimerNodeBase
  {
    public override bool GetResult(float deltaTime)
    {
      Timer.UpdateTime(deltaTime);
      return true;
    }
  }
}