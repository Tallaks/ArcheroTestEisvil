using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay.Pause
{
  public class PauseButton : MonoBehaviour
  {
    [SerializeField] private Button _button;

    private bool _isPaused;
    private IPauseService _pauseService;

    public void Initialize(IPauseService pauseService)
    {
      _pauseService = pauseService;
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