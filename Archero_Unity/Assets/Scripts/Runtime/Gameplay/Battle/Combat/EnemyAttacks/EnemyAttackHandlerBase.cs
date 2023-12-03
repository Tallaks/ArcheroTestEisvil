using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public abstract class EnemyAttackHandlerBase : MonoBehaviour, IDisposable
  {
    protected IGameplayPrefabProvider GameplayPrefabProvider;
    protected IVisualEffectPerformer VisualEffectPerformer;
    protected TransformContainer TransformContainer;
    public float Cooldown { get; protected set; }
    protected bool IsInitialized { get; private set; }
    public EnemyBehaviour Owner { get; private set; }

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

    public virtual EnemyAttackHandlerBase FinishInitialization()
    {
      if (!IsInitialized)
        throw new InvalidOperationException(
          "Cannot initialize EnemyAttackHandlerBase without calling StartInitialization first");
      return this;
    }

    public abstract void Dispose();
    public abstract void Attack(Vector3 hero);
  }
}