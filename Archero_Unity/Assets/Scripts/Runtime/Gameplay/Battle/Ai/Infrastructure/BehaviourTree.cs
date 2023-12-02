using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Decorators;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Timers;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure
{
  public class BehaviourTree : MonoBehaviour
  {
    public RootNode RootNode;

    public void Initialize(EnemyBehaviour enemyBehaviour, ICharacterRegistry characterRegistry,
      IVisibilityService visibilityService)
    {
      var initializationParameters = new NodeInitializationParams(enemyBehaviour, characterRegistry, visibilityService,
        new Dictionary<TimerTypes, TimerInitializationParams>
        {
          [TimerTypes.WaitForAttack] = new()
          {
            Duration = enemyBehaviour.AttackHandler.Cooldown
          }
        });

      if (enemyBehaviour.AttackHandler is EnemyAimedAttackHandler attackHandler)
        initializationParameters.TimersInitializationParams.Add(TimerTypes.Aim, new TimerInitializationParams
        {
          Duration = attackHandler.AimDurationSec
        });
      RootNode.Initialize(initializationParameters);
    }

    public void Clear()
    {
      RootNode = null;
    }
  }
}