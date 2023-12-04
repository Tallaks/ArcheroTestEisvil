using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai
{
  public class BehaviourTreeBrainBehaviourBaseBehaviour : EnemyBrainBehaviourBase
  {
    [field: SerializeField] private BehaviourTree BehaviourTree { get; set; }

    public override void Initialize(EnemyBehaviour owner, ICharacterRegistry characterRegistry,
      IVisibilityService visibilityService)
    {
      BehaviourTree.Initialize(owner, characterRegistry, visibilityService);
    }

    public override void UpdateBehaviour(float deltaTime)
    {
      BehaviourTree.GetResult(deltaTime);
    }

    public override void Dispose()
    {
      BehaviourTree.Clear();
    }
  }
}