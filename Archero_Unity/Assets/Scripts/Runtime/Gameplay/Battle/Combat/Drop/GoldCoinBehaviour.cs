using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Drop
{
  public class GoldCoinBehaviour : ItemBehaviourBase
  {
    protected override void GetPickedUp()
    {
      Debug.Log("Gold coin picked up");
    }
  }
}