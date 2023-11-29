using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
  [NoReflectionBaking]
  public class InstanceProvider : IProvider
  {
    private readonly object _instance;
    private readonly Type _instanceType;
    private readonly DiContainer _container;
    private readonly Action<InjectContext, object> _instantiateCallback;

    public bool IsCached => true;

    public bool TypeVariesBasedOnMemberType => false;

    public InstanceProvider(
      Type instanceType, object instance, DiContainer container, Action<InjectContext, object> instantiateCallback)
    {
      _instanceType = instanceType;
      _instance = instance;
      _container = container;
      _instantiateCallback = instantiateCallback;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return _instanceType;
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.That(args.Count == 0);
      Assert.IsNotNull(context);

      Assert.That(_instanceType.DerivesFromOrEqual(context.MemberType));

      injectAction = () =>
      {
        object instance = _container.LazyInject(_instance);

        if (_instantiateCallback != null)
          _instantiateCallback(context, instance);
      };

      buffer.Add(_instance);
    }
  }
}