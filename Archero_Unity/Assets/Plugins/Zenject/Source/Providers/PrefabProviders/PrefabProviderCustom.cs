#if !NOT_UNITY3D

using ModestTree;
using UnityEngine;

namespace Zenject
{
  [NoReflectionBaking]
  public class PrefabProviderCustom : IPrefabProvider
  {
    private readonly System.Func<InjectContext, Object> _getter;

    public PrefabProviderCustom(System.Func<InjectContext, Object> getter) =>
      _getter = getter;

    public Object GetPrefab(InjectContext context)
    {
      Object prefab = _getter(context);
      Assert.That(prefab != null, "Custom prefab provider returned null");
      return prefab;
    }
  }
}

#endif