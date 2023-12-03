using System;
using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Drop;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class EnemyBehaviour : DamageableBehaviour, IDisposable
  {
    [field: SerializeField] public EnemyMovementBehaviourBase Movement { get; private set; }
    [field: SerializeField] public EnemyBrainBehaviourBase Brain { get; private set; }
    [field: SerializeField] public EnemyAttackHandlerBase AttackHandler { get; private set; }
    [field: SerializeField] public EnemyCollisionAttackHandler CollisionHandler { get; private set; }
    [field: SerializeField] public HitBox HitBox { get; private set; }
    public int BaseDamage { get; private set; }
    private ItemDropper _itemDropper;

    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
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

    public void Initialize(ICharacterRegistry characterRegistry, IVisibilityService visibilityService,
      IEnemyAttackHandlerBuilder enemyAttackHandlerBuilder)
    {
      if (_isInitialized)
        throw new InvalidOperationException("Cannot initialize EnemyBehaviour twice");
      _isInitialized = true;
      enabled = true;
      _characterRegistry = characterRegistry;
      Health.OnDead += Die;
      HitBox.Initialize(this);
      enemyAttackHandlerBuilder.Build(this, CollisionHandler);
      if (Movement != null)
        Movement.Initialize(this);
      if (AttackHandler != null)
        enemyAttackHandlerBuilder.Build(this, AttackHandler);
      Brain.Initialize(this, characterRegistry, visibilityService);
    }

    public override void Die()
    {
      _characterRegistry.RegisterEnemyDeath();
      enabled = false;
      Brain.Dispose();
      CollisionHandler.Dispose();
      HitBox.enabled = false;
      if (Movement != null)
        Movement.Dispose();
      if (AttackHandler != null)
        AttackHandler.Dispose();

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

      Dispose();
    }

    public void ApplyProperties(EnemyConfig config, ICharacterRegistry characterRegistry,
      TransformContainer transformContainer)
    {
      BaseDamage = config.BaseDamage;
      Health = new Health(config.MaxHealth);
      _itemDropper = new ItemDropper(config.DroppedItems, characterRegistry, transformContainer);
      Health.OnDead += DropItems;
    }

    private void DropItems()
    {
      _itemDropper.DropItems(Position);
      _itemDropper.Dispose();
    }

    public void Dispose()
    {
      Health.OnDead -= Die;
      Health.OnDead -= DropItems;
    }
  }
}