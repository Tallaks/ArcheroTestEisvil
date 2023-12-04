using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using UnityEngine;
using UnityEngine.UI;

namespace Tallaks.ArcheroTest.Runtime.UI.Gameplay.Battle
{
  public class HealthBarUi : MonoBehaviour
  {
    [SerializeField] private Slider _slider;

    private IDamageable _damageableBehaviour;

    private void OnDestroy()
    {
      _damageableBehaviour.Health.OnHealthChanged -= OnHealthChanged;
      _damageableBehaviour.Health.OnDead -= OnDead;
    }

    public void Initialize(IDamageable damageableBehaviour)
    {
      _damageableBehaviour = damageableBehaviour;
      _slider.value = (float)_damageableBehaviour.Health.Current / _damageableBehaviour.Health.Max;
      _damageableBehaviour.Health.OnHealthChanged += OnHealthChanged;
      _damageableBehaviour.Health.OnDead += OnDead;
    }

    private void OnDead()
    {
      Destroy(gameObject);
    }

    private void OnHealthChanged(int newValue, int oldValue)
    {
      _slider.value = (float)newValue / _damageableBehaviour.Health.Max;
    }
  }
}