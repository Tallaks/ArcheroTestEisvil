using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class EnemyBehaviour : DamageableBehaviour
  {
    [field: SerializeField] public EnemyMovementBehaviourBase Movement { get; private set; }
    [field: SerializeField] public EnemyBrainBehaviourBase Brain { get; private set; }
    [field: SerializeField] public EnemyAttackHandlerBase AttackHandler { get; private set; }
    [field: SerializeField] public HitBox HitBox { get; private set; }

    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    private bool _isInitialized;

    private void Update()
    {
      Brain.UpdateBehaviour(Time.deltaTime);
    }

    public void Initialize(EnemyConfig config, ICharacterRegistry characterRegistry,
      IVisibilityService visibilityService)
    {
      characterRegistry.RegisterEnemy(this);
      Movement.Initialize(this);
      AttackHandler.Initialize(this);
      Health = new Health(config.MaxHealth);
      Health.OnHealthChanged += _ => Debug.Log(Health.Current);
      Health.OnDead += () => Debug.Log("Enemy died");
      HitBox.Initialize(this);
      Brain.Initialize(this, characterRegistry, visibilityService);
    }
  }
}