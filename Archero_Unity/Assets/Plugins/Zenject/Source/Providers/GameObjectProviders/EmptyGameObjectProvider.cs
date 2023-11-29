#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
  [NoReflectionBaking]
  public class EmptyGameObjectProvider : IProvider
  {
    private readonly DiContainer _container;
    private readonly GameObjectCreationParameters _gameObjectBindInfo;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    public EmptyGameObjectProvider(
      DiContainer container, GameObjectCreationParameters gameObjectBindInfo)
    {
      _gameObjectBindInfo = gameObjectBindInfo;
      _container = container;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return typeof(GameObject);
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.IsEmpty(args);

      injectAction = null;

      GameObject gameObj = _container.CreateEmptyGameObject(_gameObjectBindInfo, context);
      buffer.Add(gameObj);
    }
  }
}

#endif