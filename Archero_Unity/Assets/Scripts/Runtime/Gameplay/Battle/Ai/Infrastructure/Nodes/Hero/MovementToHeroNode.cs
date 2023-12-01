using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Hero
{
  [Serializable]
  public class MovementToHeroNode : HeroNodeBase
  {
    private EnemyMovementBehaviourBase _movement;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      base.Initialize(initializationParams);
      _movement = initializationParams.Owner.Movement;
    }

    public override bool GetResult(float deltaTime)
    {
      _movement.MoveTo(Hero.Position);
      return true;
    }
  }
}