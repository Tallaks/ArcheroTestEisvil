using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public interface IHeroAttackHandler : IDisposable
  {
    public void Initialize(
      HeroConfig.DefaultAttackDirection attackDirection, HeroBehaviour owner, IHeroAttackSystem attackSystem,
      IVisualEffectPerformer visualEffectPerformer,
      IGameplayPrefabProvider gameplayPrefabProvider, ITargetPicker targetPicker,
      TransformContainer transformContainer);

    public void Attack(Vector3 targetPosition);
  }
}