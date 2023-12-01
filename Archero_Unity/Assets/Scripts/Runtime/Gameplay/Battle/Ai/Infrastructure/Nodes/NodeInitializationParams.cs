using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Timers;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes
{
  public class NodeInitializationParams
  {
    public readonly Dictionary<TimerTypes, TimerInitializationParams> TimersInitializationParams;
    public readonly EnemyBehaviour Owner;
    public readonly HeroBehaviour Hero;
    public readonly IVisibilityService VisibilityService;

    public NodeInitializationParams(
      EnemyBehaviour owner,
      ICharacterRegistry characterRegistry,
      IVisibilityService visibilityService,
      Dictionary<TimerTypes, TimerInitializationParams> timersInitializationParams)
    {
      TimersInitializationParams = timersInitializationParams;
      VisibilityService = visibilityService;
      Owner = owner;
      Hero = characterRegistry.Hero;
    }
  }
}