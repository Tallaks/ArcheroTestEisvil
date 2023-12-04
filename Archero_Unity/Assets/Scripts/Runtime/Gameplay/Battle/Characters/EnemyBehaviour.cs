using System;
using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Drop;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks.Factory;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs;
using Tallaks.ArcheroTest.Runtime.UI.Gameplay.Battle;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class EnemyBehaviour : DamageableBehaviour, IPauseHandler, IDisposable
  {
    [field: SerializeField] public EnemyMovementBehaviourBase Movement { get; private set; }
    [field: SerializeField] public EnemyBrainBehaviourBase Brain { get; private set; }
    [field: SerializeField] public EnemyAttackHandlerBase AttackHandler { get; private set; }
    [field: SerializeField] public EnemyCollisionAttackHandler CollisionHandler { get; private set; }
    [field: SerializeField] public HitBox HitBox { get; private set; }
    [field: SerializeField] private HealthBarUi HpBar { get; set; }
    public int BaseDamage { get; private set; }
    public float MaxDistanceMovedByState { get; private set; }
    public float Speed { get; protected set; }

    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    public float AttackCooldown { get; private set; }
    private ICharacterRegistry _characterRegistry;
    private bool _isInitialized;
    private ItemDropper _itemDropper;
    private IPauseService _pauseService;

    private void Awake()
    {
      enabled = false;
      Movement.enabled = false;
    }

    private void Update()
    {
      Brain.UpdateBehaviour(Time.deltaTime);
    }

    public void Initialize(ICharacterRegistry characterRegistry, IPauseService pauseService,
      IVisibilityService visibilityService,
      IEnemyAttackHandlerBuilder enemyAttackHandlerBuilder)
    {
      if (_isInitialized)
        throw new InvalidOperationException("Cannot initialize EnemyBehaviour twice");
      _isInitialized = true;
      enabled = true;
      _characterRegistry = characterRegistry;
      _pauseService = pauseService;
      _pauseService.Register(this);
      _characterRegistry.Hero.Health.OnDead += OnHeroDead;
      Health.OnDead += Die;
      HitBox.Initialize(this);
      enemyAttackHandlerBuilder.Build(this, CollisionHandler);
      if (Movement != null)
        Movement.Initialize(this);
      if (HpBar != null)
        HpBar.Initialize(this);
      if (AttackHandler != null)
        enemyAttackHandlerBuilder.Build(this, AttackHandler);
      Brain.Initialize(this, characterRegistry, visibilityService);
    }

    public void Dispose()
    {
      Health.OnDead -= Die;
      Health.OnDead -= DropItems;
      _characterRegistry.Hero.Health.OnDead -= OnHeroDead;
    }

    public void OnPause()
    {
      enabled = false;
      Movement.OnPause();
      CollisionHandler.OnPause();
    }

    public void OnResume()
    {
      enabled = true;
      Movement.OnResume();
      CollisionHandler.OnResume();
    }

    public override void Die()
    {
      _characterRegistry.RegisterEnemyDeath();
      enabled = false;
      Brain.Dispose();
      CollisionHandler.Dispose();
      HitBox.enabled = false;
      _pauseService.Unregister(this);
      if (Movement != null)
        Movement.Dispose();
      if (AttackHandler != null)
        AttackHandler.Dispose();

      StartCoroutine(MoveDownRoutine());
    }

    public void ApplyProperties(EnemyConfig config, ICharacterRegistry characterRegistry,
      TransformContainer transformContainer)
    {
      BaseDamage = config.BaseDamage;
      Health = new Health(config.MaxHealth);
      AttackCooldown = config.MaxDistanceMovedByState / config.Speed;
      MaxDistanceMovedByState = config.MaxDistanceMovedByState;
      Speed = config.Speed;
      _itemDropper = new ItemDropper(config.DroppedItems, characterRegistry, transformContainer);
      Health.OnDead += DropItems;
    }

    private void OnHeroDead()
    {
      enabled = false;
      Brain.Dispose();
      CollisionHandler.Dispose();
      HitBox.enabled = false;
      _pauseService.Unregister(this);
      if (Movement != null)
        Movement.Dispose();
      if (AttackHandler != null)
        AttackHandler.Dispose();
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

    private void DropItems()
    {
      _itemDropper.DropItems(Position);
      _itemDropper.Dispose();
    }
  }
}