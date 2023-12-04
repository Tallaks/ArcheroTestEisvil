using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Movement
{
  [Serializable]
  public class LookAtHeroNode : MovementNodeBase
  {
    private HeroBehaviour _hero;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      base.Initialize(initializationParams);
      _hero = initializationParams.Hero;
    }

    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      Movement.LookAt(_hero.Position);
      return true;
    }
  }
}