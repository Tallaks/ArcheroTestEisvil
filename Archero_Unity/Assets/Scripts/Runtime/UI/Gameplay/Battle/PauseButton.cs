using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay.Battle
{
  public class PauseButton : MonoBehaviour
  {
    [SerializeField] private Button _button;
    private bool _isPaused;
    [Inject] private IPauseService _pauseService;

    private void Awake()
    {
      _button.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnPauseButtonClicked()
    {
      _isPaused = !_isPaused;
      if (_isPaused)
        _pauseService.Pause();
      else
        _pauseService.Resume();
    }
  }
}