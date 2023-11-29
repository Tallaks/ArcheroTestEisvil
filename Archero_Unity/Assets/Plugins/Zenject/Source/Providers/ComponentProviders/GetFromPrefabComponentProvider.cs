#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
  [NoReflectionBaking]
  public class GetFromPrefabComponentProvider : IProvider
  {
    private readonly IPrefabInstantiator _prefabInstantiator;
    private readonly Type _componentType;
    private readonly bool _matchSingle;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    // if concreteType is null we use the contract type from inject context
    public GetFromPrefabComponentProvider(
      Type componentType,
      IPrefabInstantiator prefabInstantiator, bool matchSingle)
    {
      _prefabInstantiator = prefabInstantiator;
      _componentType = componentType;
      _matchSingle = matchSingle;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return _componentType;
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.IsNotNull(context);

      GameObject gameObject = _prefabInstantiator.Instantiate(context, args, out injectAction);

      // NOTE: Need to set includeInactive to true here, because prefabs are always
      // instantiated as disabled until injection occurs, so that Awake / OnEnabled is executed
      // after injection has occurred

      if (_matchSingle)
      {
        Component match = gameObject.GetComponentInChildren(_componentType, true);

        Assert.IsNotNull(match, "Could not find component with type '{0}' on prefab '{1}'",
          _componentType, _prefabInstantiator.GetPrefab(context).name);

        buffer.Add(match);
        return;
      }

      Component[] allComponents = gameObject.GetComponentsInChildren(_componentType, true);

      Assert.That(allComponents.Length >= 1,
        "Expected to find at least one component with type '{0}' on prefab '{1}'",
        _componentType, _prefabInstantiator.GetPrefab(context).name);

      buffer.AllocFreeAddRange(allComponents);
    }
  }
}

#endif