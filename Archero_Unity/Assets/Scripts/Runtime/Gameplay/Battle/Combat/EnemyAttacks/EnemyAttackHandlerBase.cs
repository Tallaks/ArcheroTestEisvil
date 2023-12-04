using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public abstract class EnemyAttackHandlerBase : MonoBehaviour, IDisposable
  {
    public float Cooldown { get; protected set; }
    protected bool IsInitialized { get; private set; }
    public EnemyBehaviour Owner { get; private set; }
    protected IGameplayPrefabProvider GameplayPrefabProvider;
    protected IPauseService PauseService;
    protected TransformContainer TransformContainer;
    protected IVisualEffectPerformer VisualEffectPerformer;

    public abstract void Dispose();

    public EnemyAttackHandlerBase StartInitialization(EnemyBehaviour owner)
    {
      IsInitialized = true;
      Owner = owner;
      return this;
    }

    public EnemyAttackHandlerBase WithProjectiles(IGameplayPrefabProvider gameplayPrefabProvider,
      IVisualEffectPerformer visualEffectPerformer, TransformContainer transformContainer)
    {
      if (!IsInitialized)
        throw new InvalidOperationException(
          "Cannot initialize EnemyAttackHandlerBase without calling StartInitialization first");
      GameplayPrefabProvider = gameplayPrefabProvider;
      VisualEffectPerformer = visualEffectPerformer;
      TransformContainer = transformContainer;
      return this;
    }

    public EnemyAttackHandlerBase WithPauseHandle(IPauseService pauseService)
    {
      if (!IsInitialized)
        throw new InvalidOperationException(
          "Cannot initialize EnemyAttackHandlerBase without calling StartInitialization first");
      PauseService = pauseService;
      return this;
    }

    public virtual EnemyAttackHandlerBase FinishInitialization()
    {
      if (!IsInitialized)
        throw new InvalidOperationException(
          "Cannot initialize EnemyAttackHandlerBase without calling StartInitialization first");
      return this;
    }

    public abstract void Attack(Vector3 hero);
  }
}