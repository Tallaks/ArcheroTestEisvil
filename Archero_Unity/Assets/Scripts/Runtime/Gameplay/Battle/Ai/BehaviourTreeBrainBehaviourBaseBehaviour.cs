using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai
{
  public class BehaviourTreeBrainBehaviourBaseBehaviour : EnemyBrainBehaviourBase
  {
    [field: SerializeField] private BehaviourTree BehaviourTree { get; set; }

    public override void Initialize(EnemyBehaviour owner)
    {
      BehaviourTree.Initialize(owner);
    }

    public override void UpdateBehaviour(float deltaTime)
    {
      BehaviourTree.RootNode.GetResult(deltaTime);
    }
  }
}