using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage
{
  public class Health
  {
    private readonly int _max;

    public event Action OnDead;
    public event Action<int, int> OnHealthChanged;

    public int Current
    {
      get => _current;
      set
      {
        int old = _current;
        _current = Mathf.Clamp(value, 0, _max);
        OnHealthChanged?.Invoke(_current, old);
        if (_current == 0)
          OnDead?.Invoke();
      }
    }

    private int _current;

    public Health(int max)
    {
      _max = max;
      Current = max;
    }
  }
}