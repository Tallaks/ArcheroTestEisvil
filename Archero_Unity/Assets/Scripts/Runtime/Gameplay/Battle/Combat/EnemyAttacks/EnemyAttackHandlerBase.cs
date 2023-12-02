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
    public float Cooldown { get; protected set; }

    public abstract void Initialize(EnemyBehaviour owner, IGameplayPrefabProvider gameplayPrefabProvider,
      IVisualEffectPerformer visualEffectPerformer, TransformContainer transformContainer);

    public abstract void Dispose();
    public abstract void Attack(Vector3 hero);
  }
}