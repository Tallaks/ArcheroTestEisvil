using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public class HeroAttackSystem : IHeroAttackSystem
  {
    private readonly List<IHeroAttackHandler> _allAttackHandlers = new();
    private readonly List<ICooldownAttackHandler> _cooldownAttackHandlers = new();
    private readonly List<IDamageApplier> _damageAppliers = new();

    public IEnumerable<IDamageApplier> DamageAppliers => _damageAppliers;

    private CancellationTokenSource _cancellationTokenSource;

    public async UniTaskVoid StartWorking()
    {
      _cancellationTokenSource = new CancellationTokenSource();
      await foreach (AsyncUnit _ in UniTaskAsyncEnumerable.EveryUpdate()
                       .WithCancellation(_cancellationTokenSource.Token))
        for (var i = 0; i < _cooldownAttackHandlers.Count; i++)
          _cooldownAttackHandlers[i].Update(Time.deltaTime);
    }

    public void AddDamageApplier(IDamageApplier damageApplier)
    {
      _damageAppliers.Add(damageApplier);
    }

    public void StopWorking()
    {
      _cancellationTokenSource.Cancel();
    }

    public void AddAttackHandler(IHeroAttackHandler attackHandler)
    {
      _allAttackHandlers.Add(attackHandler);
      if (attackHandler is ICooldownAttackHandler cooldownAttackHandler)
        _cooldownAttackHandlers.Add(cooldownAttackHandler);
    }

    public void Dispose()
    {
      _cancellationTokenSource?.Cancel();
      _cancellationTokenSource?.Dispose();
      for (var i = 0; i < _allAttackHandlers.Count; i++)
        _allAttackHandlers[i].Dispose();
      _allAttackHandlers.Clear();
      _cooldownAttackHandlers.Clear();
      _damageAppliers.Clear();
    }
  }
}