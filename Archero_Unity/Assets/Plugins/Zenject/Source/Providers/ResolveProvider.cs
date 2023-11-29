using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
  [NoReflectionBaking]
  public class ResolveProvider : IProvider
  {
    private readonly object _identifier;
    private readonly DiContainer _container;
    private readonly Type _contractType;
    private readonly bool _isOptional;
    private readonly InjectSources _source;
    private readonly bool _matchAll;

    public bool IsCached => false;

    public bool TypeVariesBasedOnMemberType => false;

    public ResolveProvider(
      Type contractType, DiContainer container, object identifier,
      bool isOptional, InjectSources source, bool matchAll)
    {
      _contractType = contractType;
      _identifier = identifier;
      _container = container;
      _isOptional = isOptional;
      _source = source;
      _matchAll = matchAll;
    }

    public Type GetInstanceType(InjectContext context)
    {
      return _contractType;
    }

    public void GetAllInstancesWithInjectSplit(
      InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
    {
      Assert.IsEmpty(args);
      Assert.IsNotNull(context);

      Assert.That(_contractType.DerivesFromOrEqual(context.MemberType));

      injectAction = null;
      if (_matchAll)
        _container.ResolveAll(GetSubContext(context), buffer);
      else
        buffer.Add(_container.Resolve(GetSubContext(context)));
    }

    private InjectContext GetSubContext(InjectContext parent)
    {
      InjectContext subContext = parent.CreateSubContext(_contractType, _identifier);

      subContext.SourceType = _source;
      subContext.Optional = _isOptional;

      return subContext;
    }
  }
}