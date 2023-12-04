using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay.Pause
{
  public class PauseMenuUi : MonoBehaviour
  {
    [SerializeField] private Button _restartButton;
    private ICurtainService _curtainService;
    private ISceneLoader _sceneLoader;

    public void Initialize(ISceneLoader sceneLoader, ICurtainService curtainService)
    {
      _sceneLoader = sceneLoader;
      _curtainService = curtainService;
      _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }

    private void OnRestartButtonClicked()
    {
      _curtainService.Show();
      _sceneLoader.LoadScene(SceneNames.Gameplay);
    }
  }
}