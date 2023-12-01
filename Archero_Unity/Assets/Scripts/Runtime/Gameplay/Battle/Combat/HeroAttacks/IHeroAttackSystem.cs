using System;
using Cysharp.Threading.Tasks;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public interface IHeroAttackSystem : IDisposable
  {
    UniTaskVoid StartWorking();
    void AddAttackHandler(IHeroAttackHandler attackHandler);
    void StopWorking();
  }
}