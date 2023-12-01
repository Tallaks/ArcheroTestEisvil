using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Decorators;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Timers;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure
{
  public class BehaviourTree : MonoBehaviour
  {
    public RootNode RootNode;

    public void Initialize(EnemyBehaviour enemyBrainBehaviour)
    {
      var initializationParameters = new NodeInitializationParams(new Dictionary<TimerTypes, TimerInitializationParams>
      {
        [TimerTypes.WaitForAttack] = new()
        {
          Duration = enemyBrainBehaviour.AttackHandler.Cooldown
        }
      });
      RootNode.Initialize(initializationParameters);
    }
  }
}