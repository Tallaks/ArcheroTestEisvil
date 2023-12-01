using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Hero
{
  [Serializable]
  public class StopAimingNode : HeroNodeBase
  {
    private EnemyAimedAttackHandler _aimedAttackHandler;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      if (initializationParams.Owner.AttackHandler is not EnemyAimedAttackHandler enemyAimedAttackHandler)
        Debug.LogError("AimAtHeroNode can only be used with EnemyAimedAttackHandler");
      else
        _aimedAttackHandler = enemyAimedAttackHandler;
      base.Initialize(initializationParams);
    }

    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      _aimedAttackHandler.AimingDrawer.HideAimingLine();
      return true;
    }
  }
}