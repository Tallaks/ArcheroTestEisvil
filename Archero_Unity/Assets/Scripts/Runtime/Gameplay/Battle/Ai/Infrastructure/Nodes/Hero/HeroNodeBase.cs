using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Hero
{
  [Serializable]
  public abstract class HeroNodeBase : NodeBase
  {
    protected HeroBehaviour Hero;
    protected EnemyBehaviour Owner;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      Owner = initializationParams.Owner;
      Hero = initializationParams.Hero;
    }
  }
}