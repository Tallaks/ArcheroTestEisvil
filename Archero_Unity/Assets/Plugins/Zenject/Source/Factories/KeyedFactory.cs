using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;

namespace Zenject
{
  public abstract class KeyedFactoryBase<TBase, TKey> : IValidatable
  {
    [Inject] private readonly DiContainer _container;

    [InjectOptional] private readonly List<ValuePair<TKey, Type>> _typePairs;

    [InjectOptional] private readonly Type _fallbackType;

    protected DiContainer Container => _container;

    protected abstract IEnumerable<Type> ProvidedTypes { get; }

    public ICollection<TKey> Keys => TypeMap.Keys;

    protected Dictionary<TKey, Type> TypeMap { get; private set; }

    [Inject]
    public void Initialize()
    {
      Assert.That(_fallbackType == null || _fallbackType.DerivesFromOrEqual<TBase>(),
        "Expected fallback type '{0}' to derive from '{1}'", _fallbackType, typeof(TBase));

#if UNITY_EDITOR
      IEnumerable<TKey> duplicates = _typePairs.Select(x => x.First).GetDuplicates();

      if (!duplicates.IsEmpty())
        throw Assert.CreateException(
          "Found duplicate values in KeyedFactory: {0}", duplicates.Select(x => x.ToString()).Join(", "));
#endif

      TypeMap = _typePairs.ToDictionary(x => x.First, x => x.Second);
      _typePairs.Clear();
    }

    public virtual void Validate()
    {
      foreach (Type constructType in TypeMap.Values)
        Container.InstantiateExplicit(
          constructType, ValidationUtil.CreateDefaultArgs(ProvidedTypes.ToArray()));
    }

    public bool HasKey(TKey key)
    {
      return TypeMap.ContainsKey(key);
    }

    protected Type GetTypeForKey(TKey key)
    {
      Type keyedType;

      if (!TypeMap.TryGetValue(key, out keyedType))
      {
        Assert.IsNotNull(_fallbackType, "Could not find instance for key '{0}'", key);
        return _fallbackType;
      }

      return keyedType;
    }

    protected static ConditionCopyNonLazyBinder AddBindingInternal<TDerived>(DiContainer container, TKey key)
      where TDerived : TBase
    {
      return container.Bind<ValuePair<TKey, Type>>()
        .FromInstance(ValuePair.New(key, typeof(TDerived)));
    }
  }

  // Zero parameters
  public class KeyedFactory<TBase, TKey> : KeyedFactoryBase<TBase, TKey>
  {
    protected override IEnumerable<Type> ProvidedTypes => new Type[0];

    public virtual TBase Create(TKey key)
    {
      Type type = GetTypeForKey(key);
      return (TBase)Container.Instantiate(type);
    }
  }

  // One parameter
  public class KeyedFactory<TBase, TKey, TParam1> : KeyedFactoryBase<TBase, TKey>
  {
    protected override IEnumerable<Type> ProvidedTypes => new[] { typeof(TParam1) };

    public virtual TBase Create(TKey key, TParam1 param1)
    {
      return (TBase)Container.InstantiateExplicit(
        GetTypeForKey(key),
        new List<TypeValuePair>
        {
          InjectUtil.CreateTypePair(param1)
        });
    }
  }

  // Two parameters
  public class KeyedFactory<TBase, TKey, TParam1, TParam2> : KeyedFactoryBase<TBase, TKey>
  {
    protected override IEnumerable<Type> ProvidedTypes => new[] { typeof(TParam1), typeof(TParam2) };

    public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2)
    {
      return (TBase)Container.InstantiateExplicit(
        GetTypeForKey(key),
        new List<TypeValuePair>
        {
          InjectUtil.CreateTypePair(param1),
          InjectUtil.CreateTypePair(param2)
        });
    }
  }

  // Three parameters
  public class KeyedFactory<TBase, TKey, TParam1, TParam2, TParam3> : KeyedFactoryBase<TBase, TKey>
  {
    protected override IEnumerable<Type> ProvidedTypes => new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) };

    public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2, TParam3 param3)
    {
      return (TBase)Container.InstantiateExplicit(
        GetTypeForKey(key),
        new List<TypeValuePair>
        {
          InjectUtil.CreateTypePair(param1),
          InjectUtil.CreateTypePair(param2),
          InjectUtil.CreateTypePair(param3)
        });
    }
  }

  // Four parameters
  public class KeyedFactory<TBase, TKey, TParam1, TParam2, TParam3, TParam4> : KeyedFactoryBase<TBase, TKey>
  {
    protected override IEnumerable<Type> ProvidedTypes =>
      new[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4) };

    public virtual TBase Create(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
    {
      return (TBase)Container.InstantiateExplicit(
        GetTypeForKey(key),
        new List<TypeValuePair>
        {
          InjectUtil.CreateTypePair(param1),
          InjectUtil.CreateTypePair(param2),
          InjectUtil.CreateTypePair(param3),
          InjectUtil.CreateTypePair(param4)
        });
    }
  }
}