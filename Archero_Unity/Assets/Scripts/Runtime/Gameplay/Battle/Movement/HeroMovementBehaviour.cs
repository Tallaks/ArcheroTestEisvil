using System.Collections;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class HeroMovementBehaviour : MonoBehaviour
  {
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;
    private IInputService _inputService;

    private Coroutine _movementRoutine;
    private Vector2 _startPointerPosition;

    public void Initialize(IInputService inputService)
    {
      _inputService = inputService;
      inputService.OnMovementStarted += OnMovementStarted;
      inputService.OnMovementEnded += OnMovementEnded;
    }

    private void OnMovementEnded()
    {
      if (_movementRoutine != null)
        StopCoroutine(_movementRoutine);
    }

    private void OnMovementStarted()
    {
      _startPointerPosition = _inputService.PointerPosition;
      _movementRoutine = StartCoroutine(MovementRoutine());
    }

    private IEnumerator MovementRoutine()
    {
      while (true)
      {
        Vector3 movementDirection = (_inputService.PointerPosition - _startPointerPosition).FromXYToXZ().normalized;
        _characterController.Move(movementDirection * (_speed * Time.deltaTime));
        transform.LookAt(_characterController.transform.position + movementDirection);
        yield return null;
      }
    }
  }
}