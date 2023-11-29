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
  public class PrefabInstantiator : IPrefabInstantiator
  {
    private readonly IPrefabProvider _prefabProvider;
    private readonly DiContainer _container;
    private readonly List<Type> _instantiateCallbackTypes;
    private readonly Action<InjectContext, object> _instantiateCallback;

    public GameObjectCreationParameters GameObjectCreationParameters { get; }

    public Type ArgumentTarget { get; }

    public List<TypeValuePair> ExtraArguments { get; }

    public PrefabInstantiator(
      DiContainer container,
      GameObjectCreationParameters gameObjectBindInfo,
      Type argumentTarget,
      IEnumerable<Type> instantiateCallbackTypes,
      IEnumerable<TypeValuePair> extraArguments,
      IPrefabProvider prefabProvider,
      Action<InjectContext, object> instantiateCallback)
    {
      _prefabProvider = prefabProvider;
      ExtraArguments = extraArguments.ToList();
      _container = container;
      GameObjectCreationParameters = gameObjectBindInfo;
      ArgumentTarget = argumentTarget;
      _instantiateCallbackTypes = instantiateCallbackTypes.ToList();
      _instantiateCallback = instantiateCallback;
    }

    public UnityEngine.Object GetPrefab(InjectContext context)
    {
      return _prefabProvider.GetPrefab(context);
    }

    public GameObject Instantiate(InjectContext context, List<TypeValuePair> args, out Action injectAction)
    {
      Assert.That(ArgumentTarget == null || ArgumentTarget.DerivesFromOrEqual(context.MemberType));

      bool shouldMakeActive;
      GameObject gameObject = _container.CreateAndParentPrefab(
        GetPrefab(context), GameObjectCreationParameters, context, out shouldMakeActive);
      Assert.IsNotNull(gameObject);

      injectAction = () =>
      {
        List<TypeValuePair> allArgs = ZenPools.SpawnList<TypeValuePair>();

        allArgs.AllocFreeAddRange(ExtraArguments);
        allArgs.AllocFreeAddRange(args);

        if (ArgumentTarget == null)
          Assert.That(
            allArgs.IsEmpty(),
            "Unexpected arguments provided to prefab instantiator.  Arguments are not allowed if binding multiple components in the same binding");

        if (ArgumentTarget == null || allArgs.IsEmpty())
        {
          _container.InjectGameObject(gameObject);
        }
        else
        {
          _container.InjectGameObjectForComponentExplicit(
            gameObject, ArgumentTarget, allArgs, context, null);

          Assert.That(allArgs.Count == 0);
        }

        ZenPools.DespawnList(allArgs);

        if (shouldMakeActive && !_container.IsValidating)
        {
#if ZEN_INTERNAL_PROFILING
                    using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
          {
            gameObject.SetActive(true);
          }
        }

        if (_instantiateCallback != null)
        {
          HashSet<object> callbackObjects = ZenPools.SpawnHashSet<object>();

          foreach (Type type in _instantiateCallbackTypes)
          {
            Component obj = gameObject.GetComponentInChildren(type);

            if (obj != null)
              callbackObjects.Add(obj);
          }

          foreach (object obj in callbackObjects)
            _instantiateCallback(context, obj);

          ZenPools.DespawnHashSet(callbackObjects);
        }
      };

      return gameObject;
    }
  }
}

#endif