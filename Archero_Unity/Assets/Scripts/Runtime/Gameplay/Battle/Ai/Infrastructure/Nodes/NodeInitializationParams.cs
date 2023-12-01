using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Timers;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes
{
  public class NodeInitializationParams
  {
    public readonly Dictionary<TimerTypes, TimerInitializationParams> TimersInitializationParams;

    public NodeInitializationParams(Dictionary<TimerTypes, TimerInitializationParams> timersInitializationParams) =>
      TimersInitializationParams = timersInitializationParams;
  }
}