using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  [RequireComponent(typeof(Collider))]
  public class EnemyCollisionAttackHandler : EnemyAttackHandlerBase
  {
    private ValueDamageApplier _damageApplier;
    private CancellationTokenSource _cancellationTokenSource;

    public override EnemyAttackHandlerBase FinishInitialization()
    {
      _damageApplier = new ValueDamageApplier();
      return base.FinishInitialization();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out IDamageable hero) && hero is HeroBehaviour)
      {
        _cancellationTokenSource = new CancellationTokenSource();
        ApplyDamageTo(hero, _cancellationTokenSource.Token);
      }
    }

    private async UniTaskVoid ApplyDamageTo(IDamageable hero, CancellationToken cancellationToken = default)
    {
      while (cancellationToken.IsCancellationRequested == false)
      {
        Debug.Log($"Applying collision damage {Owner.BaseDamage} to {hero}");
        _damageApplier.ApplyDamage(hero.Health, Owner.BaseDamage);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.TryGetComponent(out IDamageable hero) && hero is HeroBehaviour)
      {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
      }
    }

    public override void Dispose()
    {
      Destroy(this);
      _cancellationTokenSource?.Dispose();
    }

    // TODO: restructure this
    public override void Attack(Vector3 hero)
    {
    }
  }
}