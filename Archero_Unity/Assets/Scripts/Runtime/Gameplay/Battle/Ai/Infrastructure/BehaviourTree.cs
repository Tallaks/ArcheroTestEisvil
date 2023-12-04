using System;
using System.Collections.Generic;
using System.Linq;
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
      TimerBehaviour[] timers = GetComponentsInChildren<TimerBehaviour>();
      Dictionary<TimerTypes, TimerBehaviour> timersByType = timers.ToDictionary(behaviour => behaviour.Type);
      Dictionary<TimerTypes, TimerInitializationParams> timerParamsByType = timersByType.ToDictionary(
        pair => pair.Key,
        pair => new TimerInitializationParams
        {
          Duration = GetDurationByType(pair.Key, enemyBehaviour, timersByType)
        });

      var initializationParameters =
        new NodeInitializationParams(enemyBehaviour, characterRegistry, visibilityService, timerParamsByType);
      RootNode.Initialize(initializationParameters);
    }

    public void GetResult(float deltaTime)
    {
      RootNode?.GetResult(deltaTime);
    }

    public void Clear()
    {
      RootNode = null;
    }

    private static float GetDurationByType(TimerTypes timerType, EnemyBehaviour enemyBehaviour,
      IReadOnlyDictionary<TimerTypes, TimerBehaviour> timersByType)
    {
      return timerType switch
      {
        TimerTypes.Move => enemyBehaviour.AttackCooldown,
        TimerTypes.Aim => enemyBehaviour.AttackHandler is EnemyAimedAttackHandler attackHandler
          ? attackHandler.AimDurationSec
          : 0f,
        TimerTypes.Wait => timersByType[timerType].OverrideDuration > 0 ? timersByType[timerType].OverrideDuration : 0f,
        TimerTypes.None => throw new ArgumentOutOfRangeException(nameof(timerType), timerType, null),
        _ => throw new ArgumentOutOfRangeException(nameof(timerType), timerType, null)
      };
    }
  }
}