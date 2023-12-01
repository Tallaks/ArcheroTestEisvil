using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Timers;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer
{
  [Serializable]
  public abstract class TimerNodeBase : NodeBase
  {
    public TimerBehaviour Timer;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      if (initializationParams.TimersInitializationParams.TryGetValue(Timer.Type,
            out TimerInitializationParams timerInitializationParams))
        Timer.Initialize(timerInitializationParams);
    }
  }
}