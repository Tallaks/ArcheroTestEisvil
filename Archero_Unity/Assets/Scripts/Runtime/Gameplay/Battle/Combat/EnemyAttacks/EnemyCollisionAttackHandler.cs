using System.Threading;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  [RequireComponent(typeof(Collider))]
  public class EnemyCollisionAttackHandler : EnemyAttackHandlerBase, IPauseHandler
  {
    private CancellationTokenSource _cancellationTokenSource;
    private ValueDamageApplier _damageApplier;
    private bool _isPaused;

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out HitBox heroHitBox) && heroHitBox.Owner is HeroBehaviour hero)
      {
        _cancellationTokenSource = new CancellationTokenSource();
        ApplyDamageTo(hero, _cancellationTokenSource.Token);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.TryGetComponent(out HitBox heroHitBox) && heroHitBox.Owner is HeroBehaviour hero)
        _cancellationTokenSource?.Cancel();
    }

    public void OnPause()
    {
      _isPaused = true;
    }

    public void OnResume()
    {
      _isPaused = false;
    }

    public override EnemyAttackHandlerBase FinishInitialization()
    {
      _damageApplier = new ValueDamageApplier();
      PauseService.Register(this);
      return base.FinishInitialization();
    }

    public override void Dispose()
    {
      if (_cancellationTokenSource is
          {
            Token:
            {
              CanBeCanceled: true
            }
          })
        _cancellationTokenSource.Cancel();
      _cancellationTokenSource?.Dispose();
      Destroy(this);
    }

    // TODO: restructure this
    public override void Attack(Vector3 hero)
    {
    }

    private async UniTaskVoid ApplyDamageTo(IDamageable hero, CancellationToken cancellationToken = default)
    {
      while (cancellationToken.IsCancellationRequested == false)
      {
        _damageApplier.ApplyDamage(hero.Health, Owner.BaseDamage);
        await WaitForSecondsUnpaused(1);
      }
    }

    private UniTask WaitForSecondsUnpaused(float seconds)
    {
      float cachedTimer = seconds;
      return UniTask.WaitUntil(() =>
      {
        if (_isPaused)
          return false;
        cachedTimer -= Time.deltaTime;
        return cachedTimer <= 0;
      });
    }
  }
}