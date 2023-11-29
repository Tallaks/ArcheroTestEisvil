#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
  [NoReflectionBaking]
  public class InstantiateOnPrefabComponentProvider : IProvider
  {
    private readonly IPrefabInstantiator _prefabInstantiator;
    private readonly Type _componentType;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    // if concreteType is null we use the contract type from inject context
    public InstantiateOnPrefabComponentProvider(
      Type componentType,
      IPrefabInstantiator prefabInstantiator)
    {
      _prefabInstantiator = prefabInstantiator;
      _componentType = componentType;
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

      Component component = gameObject.AddComponent(_componentType);

      buffer.Add(component);
    }
  }
}

#endif