using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs
{
  public class InputService : MonoBehaviour, IInputService
  {
    public event Action OnMovementEnded;

    public event Action OnMovementStarted;

    public Vector2 PointerPosition => _inputControls.Gameplay.PointerPosition.ReadValue<Vector2>();

    private Vector2 Delta
    {
      get => _delta;
      set
      {
        _delta = value;
        if (!_isMoving && _delta.magnitude > 0.1f)
        {
          _isMoving = true;
          OnMovementStarted?.Invoke();
        }
      }
    }

    private Vector2 _delta;
    private InputControls _inputControls;

    private bool _isMoving;

    private void Awake()
    {
      _inputControls = new InputControls();
      _inputControls.Enable();
    }

    private void Update()
    {
      if (_inputControls.Gameplay.MovementStop.WasPerformedThisFrame() && _isMoving)
      {
        HandleMovementEnded();
        return;
      }

      Delta = _inputControls.Gameplay.DeltaPosition.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
      OnMovementStarted = null;
      OnMovementStarted = null;
    }

    private void HandleMovementEnded()
    {
      OnMovementEnded?.Invoke();
      Delta = Vector2.zero;
      _isMoving = false;
    }
  }
}