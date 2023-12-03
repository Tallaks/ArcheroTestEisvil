using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  [RequireComponent(typeof(Collider))]
  public class EnemyCollisionAttackHandler : MonoBehaviour, IDisposable
  {
    private ValueDamageApplier _damageApplier;
    private EnemyBehaviour _owner;
    private CancellationTokenSource _cancellationTokenSource;

    public void Initialize(EnemyBehaviour owner)
    {
      _owner = owner;
      _damageApplier = new ValueDamageApplier();
    }

    private async void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out IDamageable hero) && hero is HeroBehaviour)
      {
        _cancellationTokenSource = new CancellationTokenSource();
        await ApplyDamageTo(hero, _cancellationTokenSource.Token);
      }
    }

    private async UniTask ApplyDamageTo(IDamageable hero, CancellationToken cancellationToken = default)
    {
      while (cancellationToken.IsCancellationRequested == false)
      {
        _damageApplier.ApplyDamage(hero.Health, _owner.BaseDamage);
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

    public void Dispose()
    {
      gameObject.SetActive(false);
      _cancellationTokenSource?.Dispose();
    }
  }
}