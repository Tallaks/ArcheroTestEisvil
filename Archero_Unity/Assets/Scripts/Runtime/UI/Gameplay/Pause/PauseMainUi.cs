using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay.Pause
{
  public class PauseMainUi : MonoBehaviour, IPauseHandler
  {
    [SerializeField] private PauseButton _pauseButton;
    [SerializeField] private PauseMenuUi _pauseMenu;
    private IPauseService _pauseService;

    private void OnDestroy()
    {
      _pauseService.Unregister(this);
    }

    public void Initialize(ISceneLoader sceneLoader, ICurtainService curtainService, IPauseService pauseService)
    {
      _pauseService = pauseService;
      _pauseButton.Initialize(pauseService);
      _pauseMenu.Initialize(sceneLoader, curtainService);
      pauseService.Register(this);
    }

    public void OnPause()
    {
      _pauseMenu.Show();
    }

    public void OnResume()
    {
      _pauseMenu.Hide();
    }
  }
}