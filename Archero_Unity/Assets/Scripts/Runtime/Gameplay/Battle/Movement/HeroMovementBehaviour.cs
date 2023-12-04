using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement
{
  public class HeroMovementBehaviour : MonoBehaviour
  {
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;
    public bool IsMoving => _movementRoutine != null;

    public Quaternion Rotation
    {
      get => transform.rotation;
      set => transform.rotation = value;
    }

    private Coroutine _aimRoutine;
    private IInputService _inputService;

    private Coroutine _movementRoutine;
    private Vector2 _startPointerPosition;
    private ITargetPicker _targetPicker;

    public void Initialize(IInputService inputService, ITargetPicker targetPicker)
    {
      _targetPicker = targetPicker;
      _inputService = inputService;
      inputService.OnMovementStarted += OnMovementStarted;
      inputService.OnMovementEnded += OnMovementEnded;
      StartAiming();
    }

    private void OnMovementEnded()
    {
      if (_movementRoutine != null)
      {
        StopCoroutine(_movementRoutine);
        _movementRoutine = null;
      }

      StartAiming();
    }

    private void StartAiming()
    {
      _aimRoutine = StartCoroutine(AimRoutine());
    }

    private IEnumerator AimRoutine()
    {
      while (!IsMoving)
      {
        yield return null;
        Vector3 lookAtPoint = _targetPicker.GetClosestTargetPosition(_characterController.transform.position);
        if (lookAtPoint != Vector3.zero)
        {
          Vector3 direction = (lookAtPoint - transform.position).normalized;
          float lookAngle = Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y;
          transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.up);
        }
      }
    }

    private void OnMovementStarted()
    {
      if (_aimRoutine != null)
      {
        StopCoroutine(_aimRoutine);
        _aimRoutine = null;
      }

      _startPointerPosition = _inputService.PointerPosition;
      _movementRoutine = StartCoroutine(MovementRoutine());
    }

    private IEnumerator MovementRoutine()
    {
      while (true)
      {
        Vector3 movementDirection = (_inputService.PointerPosition - _startPointerPosition).FromXYToXZ().normalized;
        _characterController.Move(movementDirection * (_speed * Time.deltaTime));
        float lookAngle = Quaternion.LookRotation(movementDirection, Vector3.up).eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.up);
        yield return null;
      }
    }
  }
}