#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
  [NoReflectionBaking]
  public class AddToCurrentGameObjectComponentProvider : IProvider
  {
    private readonly List<TypeValuePair> _extraArguments;
    private readonly object _concreteIdentifier;
    private readonly Action<InjectContext, object> _instantiateCallback;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    protected DiContainer Container { get; }

    protected Type ComponentType { get; }

    public AddToCurrentGameObjectComponentProvider(
      DiContainer container, Type componentType,
      IEnumerable<TypeValuePair> extraArguments, object concreteIdentifier,
      Action<InjectContext, object> instantiateCallback)
    {
      Assert.That(componentType.DerivesFrom<Component>());

      _extraArguments = extraArguments.ToList();
      ComponentType = componentType;
      Container = container;
      _concreteIdentifier = concreteIdentifier;
      _instantiateCallback = instantiateCallback;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return ComponentType;
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.IsNotNull(context);

      Assert.That(context.ObjectType.DerivesFrom<Component>(),
        "Object '{0}' can only be injected into MonoBehaviour's since it was bound with 'FromNewComponentSibling'. Attempted to inject into non-MonoBehaviour '{1}'",
        context.MemberType, context.ObjectType);

      object instance;

      if (!Container.IsValidating || TypeAnalyzer.ShouldAllowDuringValidation(ComponentType))
      {
        GameObject gameObj = ((Component)context.ObjectInstance).gameObject;

        Component componentInstance = gameObj.GetComponent(ComponentType);
        instance = componentInstance;

        // Use componentInstance so that it triggers unity's overloaded comparison operator
        // So if the component is there but missing then it returns null
        // (https://github.com/svermeulen/Zenject/issues/582)
        if (componentInstance != null)
        {
          injectAction = null;
          buffer.Add(instance);
          return;
        }

        instance = gameObj.AddComponent(ComponentType);
      }
      else
      {
        instance = new ValidationMarker(ComponentType);
      }

      // Note that we don't just use InstantiateComponentOnNewGameObjectExplicit here
      // because then circular references don't work

      injectAction = () =>
      {
        List<TypeValuePair> extraArgs = ZenPools.SpawnList<TypeValuePair>();

        extraArgs.AllocFreeAddRange(_extraArguments);
        extraArgs.AllocFreeAddRange(args);

        Container.InjectExplicit(instance, ComponentType, extraArgs, context, _concreteIdentifier);

        Assert.That(extraArgs.IsEmpty());
        ZenPools.DespawnList(extraArgs);

        if (_instantiateCallback != null)
          _instantiateCallback(context, instance);
      };

      buffer.Add(instance);
    }
  }
}

#endif