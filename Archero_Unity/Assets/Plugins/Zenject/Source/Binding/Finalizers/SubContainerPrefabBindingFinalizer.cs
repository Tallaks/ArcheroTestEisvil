#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
  [NoReflectionBaking]
  public class SubContainerPrefabBindingFinalizer : ProviderBindingFinalizer
  {
    private readonly object _subIdentifier;
    private readonly bool _resolveAll;
    private readonly Func<DiContainer, ISubContainerCreator> _subContainerCreatorFactory;

    public SubContainerPrefabBindingFinalizer(
      BindInfo bindInfo,
      object subIdentifier, bool resolveAll,
      Func<DiContainer, ISubContainerCreator> subContainerCreatorFactory)
      : base(bindInfo)
    {
      _subIdentifier = subIdentifier;
      _resolveAll = resolveAll;
      _subContainerCreatorFactory = subContainerCreatorFactory;
    }

    protected override void OnFinalizeBinding(DiContainer container)
    {
      if (BindInfo.ToChoice == ToChoices.Self)
      {
        Assert.IsEmpty(BindInfo.ToTypes);
        FinalizeBindingSelf(container);
      }
      else
      {
        FinalizeBindingConcrete(container, BindInfo.ToTypes);
      }
    }

    private void FinalizeBindingConcrete(DiContainer container, List<Type> concreteTypes)
    {
      ScopeTypes scope = GetScope();

      switch (scope)
      {
        case ScopeTypes.Transient:
        {
          RegisterProvidersForAllContractsPerConcreteType(
            container,
            concreteTypes,
            (_, concreteType) => new SubContainerDependencyProvider(
              concreteType, _subIdentifier,
              _subContainerCreatorFactory(container), _resolveAll));
          break;
        }
        case ScopeTypes.Singleton:
        {
          var containerCreator = new SubContainerCreatorCached(
            _subContainerCreatorFactory(container));

          RegisterProvidersForAllContractsPerConcreteType(
            container,
            concreteTypes,
            (_, concreteType) =>
              new SubContainerDependencyProvider(
                concreteType, _subIdentifier, containerCreator, _resolveAll));
          break;
        }
        default:
        {
          throw Assert.CreateException();
        }
      }
    }

    private void FinalizeBindingSelf(DiContainer container)
    {
      ScopeTypes scope = GetScope();

      switch (scope)
      {
        case ScopeTypes.Transient:
        {
          RegisterProviderPerContract(
            container,
            (_, contractType) => new SubContainerDependencyProvider(
              contractType, _subIdentifier,
              _subContainerCreatorFactory(container), _resolveAll));
          break;
        }
        case ScopeTypes.Singleton:
        {
          var containerCreator = new SubContainerCreatorCached(
            _subContainerCreatorFactory(container));

          RegisterProviderPerContract(
            container,
            (_, contractType) =>
              new SubContainerDependencyProvider(
                contractType, _subIdentifier, containerCreator, _resolveAll));
          break;
        }
        default:
        {
          throw Assert.CreateException();
        }
      }
    }
  }
}

#endif