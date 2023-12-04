using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public class HeroAttackSystem : IHeroAttackSystem
  {
    private readonly ICharacterRegistry _characterRegistry;
    private readonly IPauseService _pauseService;
    private readonly List<IHeroAttackHandler> _allAttackHandlers = new();
    private readonly List<ICooldownAttackHandler> _cooldownAttackHandlers = new();
    private readonly List<IDamageApplier> _damageAppliers = new();

    public IEnumerable<IDamageApplier> DamageAppliers => _damageAppliers;
    private CancellationTokenSource _cancellationTokenSource;
    private bool _isPaused;

    public HeroAttackSystem(ICharacterRegistry characterRegistry, IPauseService pauseService)
    {
      _pauseService = pauseService;
      _characterRegistry = characterRegistry;
      characterRegistry.OnAllEnemiesDead += Dispose;
      characterRegistry.Hero.Health.OnDead += Dispose;
    }

    public UniTaskVoid StartWorking()
    {
      _pauseService.Register(this);
      _cancellationTokenSource = new CancellationTokenSource();
      return StartWorkingInternal(_cancellationTokenSource.Token);
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
      _pauseService.Unregister(this);
      _cooldownAttackHandlers.Clear();
      for (var i = 0; i < _allAttackHandlers.Count; i++)
        _allAttackHandlers[i].Dispose();
      _allAttackHandlers.Clear();
      _cancellationTokenSource?.Cancel();
      _damageAppliers.Clear();
      _characterRegistry.OnAllEnemiesDead -= Dispose;
    }

    public void OnPause()
    {
      _cancellationTokenSource?.Cancel();
    }

    public void OnResume()
    {
      _cancellationTokenSource = new CancellationTokenSource();
      StartWorkingInternal(_cancellationTokenSource.Token);
    }

    private async UniTaskVoid StartWorkingInternal(CancellationToken cancellationToken = default)
    {
      await foreach (AsyncUnit _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(cancellationToken))
        for (var i = 0; i < _cooldownAttackHandlers.Count; i++)
          _cooldownAttackHandlers[i].Update(Time.deltaTime);
    }
  }
}