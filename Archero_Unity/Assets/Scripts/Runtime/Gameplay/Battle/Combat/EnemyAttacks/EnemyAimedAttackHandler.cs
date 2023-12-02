using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Aiming;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public abstract class EnemyAimedAttackHandler : EnemyAttackHandlerBase
  {
    [field: SerializeField] public AimingDrawerBehaviourBase AimingDrawer { get; private set; }
    [field: SerializeField] public float AimDurationSec { get; private set; }
    protected EnemyBehaviour Owner { get; private set; }

    public override void Initialize(EnemyBehaviour owner, IGameplayPrefabProvider gameplayPrefabProvider,
      TransformContainer transformContainer)
    {
      Owner = owner;
      AimingDrawer.Initialize(owner);
    }
  }
}