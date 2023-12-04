using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage
{
  public class Health
  {
    public event Action OnDead;
    public event Action<int, int> OnHealthChanged;

    public int Current
    {
      get => _current;
      set
      {
        int old = _current;
        _current = Mathf.Clamp(value, 0, Max);
        OnHealthChanged?.Invoke(_current, old);
        if (_current == 0)
          OnDead?.Invoke();
      }
    }

    public int Max { get; }

    private int _current;

    public Health(int max)
    {
      Max = max;
      Current = max;
    }
  }
}