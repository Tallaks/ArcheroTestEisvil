#if !NOT_UNITY3D

using System;
using UnityEngine;

namespace Zenject
{
  [NoReflectionBaking]
  public class TransformScopeConcreteIdArgConditionCopyNonLazyBinder : ScopeConcreteIdArgConditionCopyNonLazyBinder
  {
    protected GameObjectCreationParameters GameObjectInfo { get; }

    public TransformScopeConcreteIdArgConditionCopyNonLazyBinder(
      BindInfo bindInfo,
      GameObjectCreationParameters gameObjectInfo)
      : base(bindInfo) =>
      GameObjectInfo = gameObjectInfo;

    public ScopeConcreteIdArgConditionCopyNonLazyBinder UnderTransform(Transform parent)
    {
      GameObjectInfo.ParentTransform = parent;
      return this;
    }

    public ScopeConcreteIdArgConditionCopyNonLazyBinder UnderTransform(Func<InjectContext, Transform> parentGetter)
    {
      GameObjectInfo.ParentTransformGetter = parentGetter;
      return this;
    }

    public ScopeConcreteIdArgConditionCopyNonLazyBinder UnderTransformGroup(string transformGroupname)
    {
      GameObjectInfo.GroupName = transformGroupname;
      return this;
    }
  }
}

#endif