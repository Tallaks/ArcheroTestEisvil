using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using Tallaks.ArcheroTest.Runtime.UI.Gameplay.Pause;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay
{
  public class GameplayUi : MonoBehaviour
  {
    [SerializeField] private JoystickView _joystickView;
    [SerializeField] private BattleStarterView _battleStarterView;
    [SerializeField] private PauseMainUi _pauseMainUi;

    public void Initialize(IInputService inputService, ISceneLoader sceneLoader, ICurtainService curtainService,
      IPauseService pauseService, IBattleStarter battleStarter)
    {
      _joystickView.Initialize(inputService, pauseService);
      _battleStarterView.Initialize(battleStarter);
      _pauseMainUi.Initialize(sceneLoader, curtainService, pauseService);
    }
  }
}