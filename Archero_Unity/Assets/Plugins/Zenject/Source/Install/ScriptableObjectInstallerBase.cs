#if !NOT_UNITY3D

using System;
using UnityEngine;

namespace Zenject
{
  // We'd prefer to make this abstract but Unity 5.3.5 has a bug where references
  // can get lost during compile errors for classes that are abstract
  public class ScriptableObjectInstallerBase : ScriptableObject, IInstaller
  {
    bool IInstaller.IsEnabled => true;

    protected DiContainer Container => _container;

    [Inject] private DiContainer _container;

    public virtual void InstallBindings()
    {
      throw new NotImplementedException();
    }
  }
}

#endif