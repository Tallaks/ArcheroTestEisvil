using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay.Pause
{
  public class PauseMenuUi : MonoBehaviour
  {
    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }
  }
}