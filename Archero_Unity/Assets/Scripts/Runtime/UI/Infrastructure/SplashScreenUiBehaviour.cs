using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Infrastructure
{
  public class SplashScreenUiBehaviour : MonoBehaviour
  {
    private void Awake()
    {
      Hide();
      DontDestroyOnLoad(gameObject);
    }

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