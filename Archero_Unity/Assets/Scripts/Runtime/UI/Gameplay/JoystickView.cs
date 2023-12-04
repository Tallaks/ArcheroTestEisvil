using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay
{
  public class JoystickView : MonoBehaviour, IPauseHandler
  {
    [SerializeField] private RectTransform _pointer;
    private Coroutine _coroutine;
    private Vector3 _defaultPosition;

    private IInputService _inputService;
    private float _maxPointerOffset;
    private IPauseService _pauseService;
    private Vector2 _positionOnStartMove;

    private void OnDestroy()
    {
      _pauseService.Unregister(this);
      _inputService.OnMovementStarted -= OnMovementStarted;
      _inputService.OnMovementEnded -= OnMovementEnded;
    }

    public void Initialize(IInputService inputService, IPauseService pauseService)
    {
      _pauseService = pauseService;
      _pauseService.Register(this);
      _inputService = inputService;
      SetDefaultPositions();
      inputService.OnMovementStarted += OnMovementStarted;
      inputService.OnMovementEnded += OnMovementEnded;
    }

    public void OnPause()
    {
      StopAllCoroutines();
      SetDefaultPositions();
      _inputService.OnMovementStarted -= OnMovementStarted;
      _inputService.OnMovementEnded -= OnMovementEnded;
    }

    public void OnResume()
    {
      _inputService.OnMovementStarted += OnMovementStarted;
      _inputService.OnMovementEnded += OnMovementEnded;
    }

    private void SetDefaultPositions()
    {
      _defaultPosition = transform.position;
      _maxPointerOffset = _pointer.anchoredPosition.magnitude;
      _pointer.anchoredPosition = Vector2.zero;
    }

    private void OnMovementEnded()
    {
      transform.position = _defaultPosition;
      if (_coroutine != null)
        StopCoroutine(_coroutine);
      _pointer.anchoredPosition = Vector2.zero;
    }

    private void OnMovementStarted()
    {
      transform.position = _inputService.PointerPosition;
      _positionOnStartMove = _inputService.PointerPosition;
      _coroutine = StartCoroutine(MovePointer());
    }

    private IEnumerator MovePointer()
    {
      while (true)
      {
        Vector2 direction = _inputService.PointerPosition - _positionOnStartMove;
        if (direction.magnitude > _maxPointerOffset)
          direction = direction.normalized * _maxPointerOffset;
        _pointer.anchoredPosition = direction;
        yield return null;
      }
    }
  }
}