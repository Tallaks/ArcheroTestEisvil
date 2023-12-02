using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using TMPro;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay
{
  public class BattleStarterView : MonoBehaviour
  {
    [SerializeField] private GameObject _battleStarterRoot;
    [SerializeField] private TextMeshProUGUI _secondsLeftText;

    private IBattleStarter _battleStarter;

    public void Initialize(IBattleStarter battleStarter)
    {
      _battleStarter = battleStarter;
      _battleStarter.OnSecondsLeftChanged += OnSecondsLeftChanged;
      _secondsLeftText.text = _battleStarter.SecondsLeft.ToString();
    }

    private void OnSecondsLeftChanged(int secondsLeft)
    {
      _secondsLeftText.text = secondsLeft.ToString();
      if (secondsLeft == 0)
        _battleStarterRoot.SetActive(false);
    }
  }
}