using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs
{
  public interface IInputService
  {
    event Action OnMovementEnded;
    event Action OnMovementStarted;
    Vector2 PointerPosition { get; }
  }
}