using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause
{
  public interface IPauseService : IDisposable
  {
    void Register(IPauseHandler pauseHandler);
    void Unregister(IPauseHandler pauseHandler);
    void Pause();
    void Resume();
  }
}