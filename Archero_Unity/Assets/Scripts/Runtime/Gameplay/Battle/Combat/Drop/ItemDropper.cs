using System;
using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Drop
{
  public class ItemDropper : IDisposable
  {
    private readonly List<DroppableItems> _droppableItems;
    private readonly List<ItemBehaviourBase> _items;
    private readonly ICharacterRegistry _characterRegistry;
    private readonly TransformContainer _transformContainer;

    public ItemDropper(IEnumerable<DroppableItems> droppableItems, ICharacterRegistry characterRegistry,
      TransformContainer transformContainer)
    {
      _characterRegistry = characterRegistry;
      _transformContainer = transformContainer;
      _droppableItems = new List<DroppableItems>(droppableItems);
      _items = new List<ItemBehaviourBase>();
      CreateItems();
    }

    public void Dispose()
    {
      _droppableItems.Clear();
    }

    public void DropItems(Vector3 position)
    {
      foreach (ItemBehaviourBase item in _items)
        item.Drop(position);
    }

    private void CreateItems()
    {
      foreach (DroppableItems droppableItem in _droppableItems)
      {
        int amount = Random.Range(droppableItem.MinAmount, droppableItem.MaxAmount);
        for (var i = 0; i < amount; i++)
        {
          ItemBehaviourBase item = Object.Instantiate(droppableItem.Prefab, Vector3.down, Quaternion.identity,
            _transformContainer.DropContainer);
          item.Initialize(_characterRegistry);
          _items.Add(item);
        }
      }
    }
  }
}