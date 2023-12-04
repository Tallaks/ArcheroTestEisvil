using System.Collections.Generic;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause
{
  public class PauseService : IPauseService
  {
    private readonly List<IPauseHandler> _pauseHandlers = new(10);

    public void Register(IPauseHandler pauseHandler)
    {
      _pauseHandlers.Add(pauseHandler);
    }

    public void Unregister(IPauseHandler pauseHandler)
    {
      _pauseHandlers.Remove(pauseHandler);
    }

    public void Pause()
    {
      for (var i = 0; i < _pauseHandlers.Count; i++)
        _pauseHandlers[i].OnPause();
    }

    public void Resume()
    {
      for (var i = 0; i < _pauseHandlers.Count; i++)
        _pauseHandlers[i].OnResume();
    }

    public void Dispose()
    {
      _pauseHandlers.Clear();
    }
  }
}