using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay
{
  public class GameplayUi : MonoBehaviour
  {
    [SerializeField] private JoystickView _joystickView;
    [SerializeField] private BattleStarterView _battleStarterView;

    public void Initialize(IInputService inputService, IBattleStarter battleStarter)
    {
      _joystickView.Initialize(inputService);
      _battleStarterView.Initialize(battleStarter);
    }
  }
}