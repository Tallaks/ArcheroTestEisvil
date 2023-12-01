using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Hero
{
  [Serializable]
  public class HeroIsVisibleNode : HeroNodeBase
  {
    private IVisibilityService _visibilityService;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      base.Initialize(initializationParams);
      _visibilityService = initializationParams.VisibilityService;
    }

    public override bool GetResult(float deltaTime)
    {
      return _visibilityService.HeroIsVisibleByEnemy(Owner, Hero);
    }
  }
}