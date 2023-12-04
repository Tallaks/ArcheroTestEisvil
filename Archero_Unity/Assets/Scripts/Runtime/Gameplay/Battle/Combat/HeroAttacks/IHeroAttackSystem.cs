using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public interface IHeroAttackSystem : IPauseHandler, IDisposable
  {
    IEnumerable<IDamageApplier> DamageAppliers { get; }
    UniTaskVoid StartWorking();
    void AddAttackHandler(IHeroAttackHandler attackHandler);
    void AddDamageApplier(IDamageApplier damageApplier);
    void StopWorking();
  }
}