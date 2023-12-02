using System;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Hero
{
  [Serializable]
  public class AttackHeroNode : HeroNodeBase
  {
    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      Owner.AttackHandler.Attack(Hero.Position.WithY(PhysicsConstants.ProjectileHeight));
      return true;
    }
  }
}