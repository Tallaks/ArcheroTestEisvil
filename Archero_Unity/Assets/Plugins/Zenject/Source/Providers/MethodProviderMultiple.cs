using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
  [NoReflectionBaking]
  public class MethodProviderMultiple<TReturn> : IProvider
  {
    private readonly DiContainer _container;
    private readonly Func<InjectContext, IEnumerable<TReturn>> _method;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    public MethodProviderMultiple(
      Func<InjectContext, IEnumerable<TReturn>> method,
      DiContainer container)
    {
      _container = container;
      _method = method;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return typeof(TReturn);
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.IsEmpty(args);
      Assert.IsNotNull(context);

      Assert.That(typeof(TReturn).DerivesFromOrEqual(context.MemberType));

      injectAction = null;
      if (_container.IsValidating && !TypeAnalyzer.ShouldAllowDuringValidation(context.MemberType))
      {
        buffer.Add(new ValidationMarker(typeof(TReturn)));
      }
      else
      {
        IEnumerable<TReturn> result = _method(context);

        if (result == null)
          throw Assert.CreateException(
            "Method '{0}' returned null when list was expected. Object graph:\n {1}",
            _method.ToDebugString(), context.GetObjectGraphString());

        foreach (TReturn obj in result)
          buffer.Add(obj);
      }
    }
  }
}