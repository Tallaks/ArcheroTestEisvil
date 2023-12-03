using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Drop;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs
{
  [Serializable]
  public class DroppableItems
  {
    public string Name;
    public int MinAmount;
    public int MaxAmount;
    public ItemBehaviourBase Prefab;
  }
}