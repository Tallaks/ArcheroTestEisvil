using DamageNumbersPro;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage
{
  [RequireComponent(typeof(Collider))]
  public class HitBox : MonoBehaviour
  {
    [SerializeField] private DamageNumberMesh _damageNumberMeshPrefab;
    public IDamageable Owner { get; private set; }

    private void OnDisable()
    {
      GetComponent<Collider>().enabled = false;
    }

    public void Initialize(IDamageable owner)
    {
      Owner = owner;
      owner.Health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int newHealth, int oldHealth)
    {
      _damageNumberMeshPrefab.Spawn(transform.position, oldHealth - newHealth);
    }
  }
}