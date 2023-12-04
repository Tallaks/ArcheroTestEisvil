using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.UI.Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes
{
  public class CurtainService : ICurtainService
  {
    private SplashScreenUiBehaviour _splashScreen;

    public async UniTask InitializeAsync()
    {
      GameObject curtainGo = await Addressables.InstantiateAsync("SplashScreen");
      _splashScreen = curtainGo.GetComponent<SplashScreenUiBehaviour>();
    }

    public void Show()
    {
      _splashScreen.Show();
    }

    public void Hide()
    {
      _splashScreen.Hide();
    }
  }
}