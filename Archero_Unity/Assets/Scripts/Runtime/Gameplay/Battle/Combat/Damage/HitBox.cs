using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage
{
  [RequireComponent(typeof(Collider))]
  public class HitBox : MonoBehaviour
  {
    public IDamageable Owner { get; private set; }

    private void OnDisable()
    {
      GetComponent<Collider>().enabled = false;
    }

    public void Initialize(IDamageable owner)
    {
      Owner = owner;
    }
  }
}