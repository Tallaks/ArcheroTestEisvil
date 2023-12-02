using System;
using Cysharp.Threading.Tasks;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat
{
  public class BattleStarter : IBattleStarter
  {
    public event Action<int> OnSecondsLeftChanged;

    public int SecondsLeft { get; private set; } = 3;

    public async UniTask WaitForBattleStart()
    {
      while (SecondsLeft >= 0)
      {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        SecondsLeft--;
        OnSecondsLeftChanged?.Invoke(SecondsLeft);
      }
    }
  }
}