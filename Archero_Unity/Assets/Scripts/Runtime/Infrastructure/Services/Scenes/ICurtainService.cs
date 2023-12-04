using Cysharp.Threading.Tasks;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Scenes
{
  public interface ICurtainService
  {
    UniTask InitializeAsync();
    void Show();
    void Hide();
  }
}