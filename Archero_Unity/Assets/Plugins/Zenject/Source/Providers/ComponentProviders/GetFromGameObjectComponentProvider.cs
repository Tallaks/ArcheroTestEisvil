#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
  [NoReflectionBaking]
  public class GetFromGameObjectComponentProvider : IProvider
  {
    private readonly GameObject _gameObject;
    private readonly Type _componentType;
    private readonly bool _matchSingle;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    // if concreteType is null we use the contract type from inject context
    public GetFromGameObjectComponentProvider(
      Type componentType, GameObject gameObject, bool matchSingle)
    {
      _componentType = componentType;
      _matchSingle = matchSingle;
      _gameObject = gameObject;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return _componentType;
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.IsNotNull(context);

      injectAction = null;

      if (_matchSingle)
      {
        Component match = _gameObject.GetComponent(_componentType);

        Assert.IsNotNull(match, "Could not find component with type '{0}' on prefab '{1}'",
          _componentType, _gameObject.name);

        buffer.Add(match);
        return;
      }

      Component[] allComponents = _gameObject.GetComponents(_componentType);

      Assert.That(allComponents.Length >= 1,
        "Expected to find at least one component with type '{0}' on prefab '{1}'",
        _componentType, _gameObject.name);

      buffer.AllocFreeAddRange(allComponents);
    }
  }
}

#endif