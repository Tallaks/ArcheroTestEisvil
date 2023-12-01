using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class EnemyBehaviour : MonoBehaviour
  {
    [field: SerializeField] public EnemyMovementBehaviourBase Movement { get; private set; }
    [field: SerializeField] public EnemyBrainBehaviourBase Brain { get; private set; }
    [field: SerializeField] public EnemyAttackHandlerBase AttackHandler { get; private set; }

    public Vector3 Position => transform.position;

    private void Update()
    {
      Brain.UpdateBehaviour(Time.deltaTime);
    }

    public void Initialize(ICharacterRegistry characterRegistry, IVisibilityService visibilityService)
    {
      characterRegistry.RegisterEnemy(this);
      Brain.Initialize(this, characterRegistry, visibilityService);
      AttackHandler.Initialize(this);
    }
  }
}