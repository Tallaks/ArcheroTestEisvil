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
    private float _currentTime;

    public void Initialize(TimerInitializationParams initializationParams)
    {
      Duration = initializationParams.Duration;
    }

    public void StartRunning()
    {
      if (IsRunning)
        Debug.LogError($"Timer {Name} is already running");
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