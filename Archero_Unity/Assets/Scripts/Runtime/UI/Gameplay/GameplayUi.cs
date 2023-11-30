using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay
{
  public class GameplayUi : MonoBehaviour
  {
    [SerializeField] private JoystickView _joystickView;

    public void Initialize(IInputService inputService)
    {
      _joystickView.Initialize(inputService);
    }
  }
}