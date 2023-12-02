using System;
using Cysharp.Threading.Tasks;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat
{
  public interface IBattleStarter
  {
    event Action<int> OnSecondsLeftChanged;
    int SecondsLeft { get; }
    UniTask WaitForBattleStart();
  }
}