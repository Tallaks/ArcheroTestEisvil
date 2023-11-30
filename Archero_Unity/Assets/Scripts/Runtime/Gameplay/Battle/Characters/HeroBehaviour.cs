using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class HeroBehaviour : MonoBehaviour
  {
    [field: SerializeField] public HeroMovementBehaviour Movement { get; private set; }

    public void Initialize(IInputService inputService)
    {
      Movement.Initialize(inputService);
    }
  }
}