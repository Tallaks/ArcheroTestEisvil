#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject.Internal;

namespace Zenject
{
  public class SceneDecoratorContext : Context
  {
    private readonly List<MonoBehaviour> _injectableMonoBehaviours = new();

    [SerializeField] private List<MonoInstaller> _lateInstallers = new();

    [SerializeField] private List<MonoInstaller> _lateInstallerPrefabs = new();

    [SerializeField] private List<ScriptableObjectInstaller> _lateScriptableObjectInstallers = new();

    [FormerlySerializedAs("SceneName")] [SerializeField] private string _decoratedContractName;

    public IEnumerable<MonoInstaller> LateInstallers
    {
      get => _lateInstallers;
      set
      {
        _lateInstallers.Clear();
        _lateInstallers.AddRange(value);
      }
    }

    public IEnumerable<MonoInstaller> LateInstallerPrefabs
    {
      get => _lateInstallerPrefabs;
      set
      {
        _lateInstallerPrefabs.Clear();
        _lateInstallerPrefabs.AddRange(value);
      }
    }

    public IEnumerable<ScriptableObjectInstaller> LateScriptableObjectInstallers
    {
      get => _lateScriptableObjectInstallers;
      set
      {
        _lateScriptableObjectInstallers.Clear();
        _lateScriptableObjectInstallers.AddRange(value);
      }
    }

    public string DecoratedContractName => _decoratedContractName;

    public override DiContainer Container
    {
      get
      {
        Assert.IsNotNull(_container);
        return _container;
      }
    }

    private DiContainer _container;

    public void Initialize(DiContainer container)
    {
      Assert.IsNull(_container);
      Assert.That(_injectableMonoBehaviours.IsEmpty());

      _container = container;

      GetInjectableMonoBehaviours(_injectableMonoBehaviours);

      foreach (MonoBehaviour instance in _injectableMonoBehaviours)
        container.QueueForInject(instance);
    }

    public override IEnumerable<GameObject> GetRootGameObjects()
    {
      // This method should never be called because SceneDecoratorContext's are not bound
      // to the container
      throw Assert.CreateException();
    }

    public void InstallDecoratorSceneBindings()
    {
      _container.Bind<SceneDecoratorContext>().FromInstance(this);
      InstallSceneBindings(_injectableMonoBehaviours);
    }

    public void InstallDecoratorInstallers()
    {
      InstallInstallers();
    }

    public void InstallLateDecoratorInstallers()
    {
      InstallInstallers(new List<InstallerBase>(), new List<Type>(), _lateScriptableObjectInstallers, _lateInstallers,
        _lateInstallerPrefabs);
    }

    protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
    {
      Scene scene = gameObject.scene;

      ZenUtilInternal.AddStateMachineBehaviourAutoInjectersInScene(scene);
      ZenUtilInternal.GetInjectableMonoBehavioursInScene(scene, monoBehaviours);
    }
  }
}

#endif