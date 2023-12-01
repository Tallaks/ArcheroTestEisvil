using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Hero
{
  [Serializable]
  public class AttackHeroNode : HeroNodeBase
  {
    public override bool GetResult(float deltaTime)
    {
      Owner.AttackHandler.Attack(Hero.Position);
      return true;
    }
  }
}