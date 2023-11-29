using System;
using System.Collections.Generic;
using ModestTree;
using Zenject.Internal;

namespace Zenject
{
  [NoReflectionBaking]
  public class BindStatement : IDisposable
  {
    private readonly List<IDisposable> _disposables;

    public BindingInheritanceMethods BindingInheritanceMethod
    {
      get
      {
        AssertHasFinalizer();
        return _bindingFinalizer.BindingInheritanceMethod;
      }
    }

    public bool HasFinalizer => _bindingFinalizer != null;

    private IBindingFinalizer _bindingFinalizer;

    public BindStatement()
    {
      _disposables = new List<IDisposable>();
      Reset();
    }

    public void Dispose()
    {
      ZenPools.DespawnStatement(this);
    }

    public void SetFinalizer(IBindingFinalizer bindingFinalizer)
    {
      _bindingFinalizer = bindingFinalizer;
    }

    public void AddDisposable(IDisposable disposable)
    {
      _disposables.Add(disposable);
    }

    public BindInfo SpawnBindInfo()
    {
      BindInfo bindInfo = ZenPools.SpawnBindInfo();
      AddDisposable(bindInfo);
      return bindInfo;
    }

    public void FinalizeBinding(DiContainer container)
    {
      AssertHasFinalizer();
      _bindingFinalizer.FinalizeBinding(container);
    }

    public void Reset()
    {
      _bindingFinalizer = null;

      for (var i = 0; i < _disposables.Count; i++)
        _disposables[i].Dispose();

      _disposables.Clear();
    }

    private void AssertHasFinalizer()
    {
      if (_bindingFinalizer == null)
        throw Assert.CreateException(
          "Unfinished binding!  Some required information was left unspecified.");
    }
  }
}