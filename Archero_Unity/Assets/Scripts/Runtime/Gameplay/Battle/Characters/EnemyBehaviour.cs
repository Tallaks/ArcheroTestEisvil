using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class EnemyBehaviour : DamageableBehaviour
  {
    [field: SerializeField] public EnemyMovementBehaviourBase Movement { get; private set; }
    [field: SerializeField] public EnemyBrainBehaviourBase Brain { get; private set; }
    [field: SerializeField] public EnemyAttackHandlerBase AttackHandler { get; private set; }
    [field: SerializeField] public HitBox HitBox { get; private set; }
    public int BaseDamage { get; private set; }

    public Vector3 Position
    {
      get => transform.position;
      private set => transform.position = value;
    }

    private ICharacterRegistry _characterRegistry;

    private bool _isInitialized;

    private void Awake()
    {
      enabled = false;
    }

    private void Update()
    {
      Brain.UpdateBehaviour(Time.deltaTime);
    }

    public void Initialize(ICharacterRegistry characterRegistry, IGameplayPrefabProvider gameplayPrefabProvider,
      IVisibilityService visibilityService, TransformContainer transformContainer)
    {
      enabled = true;
      _characterRegistry = characterRegistry;
      Movement.Initialize(this);
      AttackHandler.Initialize(this, gameplayPrefabProvider, transformContainer);
      Health.OnDead += Die;
      HitBox.Initialize(this);
      Brain.Initialize(this, characterRegistry, visibilityService);
    }

    public override void Die()
    {
      _characterRegistry.RegisterEnemyDeath();
      enabled = false;
      Movement.Dispose();
      Brain.Dispose();
      AttackHandler.Dispose();
      HitBox.enabled = false;
      StartCoroutine(MoveDownRoutine());
    }

    private IEnumerator MoveDownRoutine()
    {
      Vector3 startPosition = Position;
      Vector3 targetPosition = startPosition - Vector3.up * 3;
      var t = 0f;
      while (t < 1f)
      {
        t += Time.deltaTime;
        Position = Vector3.Lerp(startPosition, targetPosition, t);
        yield return null;
      }
    }

    public void ApplyProperties(EnemyConfig config)
    {
      BaseDamage = config.BaseDamage;
      Health = new Health(config.MaxHealth);
    }
  }
}