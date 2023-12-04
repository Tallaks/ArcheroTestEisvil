using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Timer;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Timers
{
  public class TimerBehaviour : MonoBehaviour
  {
    public string Name = "Timer";
    public TimerTypes Type;
    public float Duration { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsTimeOut => _currentTime >= Duration;

    [field: SerializeField] public float OverrideDuration { get; private set; } = -1f;

    private float _currentTime;
    private bool _isInitialized;

    public void Reset()
    {
      IsRunning = false;
      _currentTime = 0;
    }

    public void Initialize(TimerInitializationParams initializationParams)
    {
      if (_isInitialized)
        return;

      _isInitialized = true;
      IsRunning = false;
      Duration = initializationParams.Duration;
    }

    public void StartRunning()
    {
      if (IsRunning)
        Debug.LogError($"Timer {Name} is already running");
      _currentTime = 0;
      IsRunning = true;
    }

    public void UpdateTime(float deltaTime)
    {
      if (!IsRunning)
        Debug.LogError($"Timer {Name} is not running and cannot be updated");

      _currentTime += deltaTime;
    }
  }
}