using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Drop
{
  [RequireComponent(typeof(Collider))]
  public class ItemPicker : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.attachedRigidbody.TryGetComponent(out ItemBehaviourBase itemBehaviour))
        Destroy(itemBehaviour.gameObject);
    }
  }
}