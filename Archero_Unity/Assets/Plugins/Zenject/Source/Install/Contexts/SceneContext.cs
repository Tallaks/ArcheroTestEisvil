#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject.Internal;

namespace Zenject
{
  public class SceneContext : RunnableContext
  {
    public static Action<DiContainer> ExtraBindingsInstallMethod;
    public static Action<DiContainer> ExtraBindingsLateInstallMethod;

    public static IEnumerable<DiContainer> ParentContainers;

    private readonly List<SceneDecoratorContext> _decoratorContexts = new();
    public event Action PostInstall;
    public event Action PostResolve;
    public event Action PreInstall;
    public event Action PreResolve;

    public UnityEvent OnPreInstall;
    public UnityEvent OnPostInstall;
    public UnityEvent OnPreResolve;
    public UnityEvent OnPostResolve;

    [FormerlySerializedAs("ParentNewObjectsUnderRoot")]
    [FormerlySerializedAs("_parentNewObjectsUnderRoot")]
    [Tooltip("When true, objects that are created at runtime will be parented to the SceneContext")]
    [SerializeField]
    private bool _parentNewObjectsUnderSceneContext;

    [Tooltip(
      "Optional contract names for this SceneContext, allowing contexts in subsequently loaded scenes to depend on it and be parented to it, and also for previously loaded decorators to be included")]
    [SerializeField]
    private List<string> _contractNames = new();

    [Tooltip(
      "Optional contract names of SceneContexts in previously loaded scenes that this context depends on and to which it should be parented")]
    [SerializeField]
    private List<string> _parentContractNames = new();

    public override DiContainer Container => _container;

    public bool HasResolved { get; private set; }

    public bool HasInstalled { get; private set; }

    public bool IsValidating => ProjectContext.Instance.Container.IsValidating;

    public IEnumerable<string> ContractNames
    {
      get => _contractNames;
      set
      {
        _contractNames.Clear();
        _contractNames.AddRange(value);
      }
    }

    public IEnumerable<string> ParentContractNames
    {
      get
      {
        var result = new List<string>();
        result.AddRange(_parentContractNames);
        return result;
      }
      set => _parentContractNames = value.ToList();
    }

    public bool ParentNewObjectsUnderSceneContext
    {
      get => _parentNewObjectsUnderSceneContext;
      set => _parentNewObjectsUnderSceneContext = value;
    }

    private DiContainer _container;

    public void Awake()
    {
#if ZEN_INTERNAL_PROFILING
            ProfileTimers.ResetAll();
            using (ProfileTimers.CreateTimedBlock("Other"))
#endif
      {
        Initialize();
      }
    }

    public void Validate()
    {
      Assert.That(IsValidating);

      Install();
      Resolve();
    }

    public override IEnumerable<GameObject> GetRootGameObjects()
    {
      return ZenUtilInternal.GetRootGameObjects(gameObject.scene);
    }

    public void Install()
    {
      Assert.That(!HasInstalled);
      HasInstalled = true;

      Assert.IsNull(_container);

      IEnumerable<DiContainer> parents = GetParentContainers();
      Assert.That(!parents.IsEmpty());
      Assert.That(parents.All(x => x.IsValidating == parents.First().IsValidating));

      _container = new DiContainer(parents, parents.First().IsValidating);

      // Do this after creating DiContainer in case it's needed by the pre install logic
      if (PreInstall != null)
        PreInstall();

      if (OnPreInstall != null)
        OnPreInstall.Invoke();

      Assert.That(_decoratorContexts.IsEmpty());
      _decoratorContexts.AddRange(LookupDecoratorContexts());

      if (_parentNewObjectsUnderSceneContext)
        _container.DefaultParent = transform;
      else
        _container.DefaultParent = null;

      // Record all the injectable components in the scene BEFORE installing the installers
      // This is nice for cases where the user calls InstantiatePrefab<>, etc. in their installer
      // so that it doesn't inject on the game object twice
      // InitialComponentsInjecter will also guarantee that any component that is injected into
      // another component has itself been injected
      var injectableMonoBehaviours = new List<MonoBehaviour>();
      GetInjectableMonoBehaviours(injectableMonoBehaviours);
      foreach (MonoBehaviour instance in injectableMonoBehaviours)
        _container.QueueForInject(instance);

      foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
        decoratorContext.Initialize(_container);

      _container.IsInstalling = true;

      try
      {
        InstallBindings(injectableMonoBehaviours);
      }
      finally
      {
        _container.IsInstalling = false;
      }

      if (PostInstall != null)
        PostInstall();

      if (OnPostInstall != null)
        OnPostInstall.Invoke();
    }

    public void Resolve()
    {
      if (PreResolve != null)
        PreResolve();

      if (OnPreResolve != null)
        OnPreResolve.Invoke();

      Assert.That(HasInstalled);
      Assert.That(!HasResolved);
      HasResolved = true;

      _container.ResolveRoots();

      if (PostResolve != null)
        PostResolve();

      if (OnPostResolve != null)
        OnPostResolve.Invoke();
    }

    // These methods can be used for cases where you need to create the SceneContext entirely in code
    // Note that if you use these methods that you have to call Run() yourself
    // This is useful because it allows you to create a SceneContext and configure it how you want
    // and add what installers you want before kicking off the Install/Resolve
    public static SceneContext Create()
    {
      return CreateComponent<SceneContext>(
        new GameObject("SceneContext"));
    }

    protected override void RunInternal()
    {
      // We always want to initialize ProjectContext as early as possible
      ProjectContext.Instance.EnsureIsInitialized();

#if UNITY_EDITOR
      using (ProfileBlock.Start("Zenject.SceneContext.Install"))
#endif
      {
        Install();
      }

#if UNITY_EDITOR
      using (ProfileBlock.Start("Zenject.SceneContext.Resolve"))
#endif
      {
        Resolve();
      }
    }

    protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
    {
      Scene scene = gameObject.scene;

      ZenUtilInternal.AddStateMachineBehaviourAutoInjectersInScene(scene);
      ZenUtilInternal.GetInjectableMonoBehavioursInScene(scene, monoBehaviours);
    }

    private IEnumerable<DiContainer> GetParentContainers()
    {
      IEnumerable<string> parentContractNames = ParentContractNames;

      if (parentContractNames.IsEmpty())
      {
        if (ParentContainers != null)
        {
          IEnumerable<DiContainer> tempParentContainer = ParentContainers;

          // Always reset after using it - it is only used to pass the reference
          // between scenes via ZenjectSceneLoader
          ParentContainers = null;

          return tempParentContainer;
        }

        return new[] { ProjectContext.Instance.Container };
      }

      Assert.IsNull(ParentContainers,
        "Scene cannot have both a parent scene context name set and also an explicit parent container given");

      List<DiContainer> parentContainers = UnityUtil.AllLoadedScenes
        .Except(gameObject.scene)
        .SelectMany(scene => scene.GetRootGameObjects())
        .SelectMany(root => root.GetComponentsInChildren<SceneContext>())
        .Where(sceneContext => sceneContext.ContractNames.Where(x => parentContractNames.Contains(x)).Any())
        .Select(x => x.Container)
        .ToList();

      if (!parentContainers.Any())
        throw Assert.CreateException(
          "SceneContext on object {0} of scene {1} requires at least one of contracts '{2}', but none of the loaded SceneContexts implements that contract.",
          gameObject.name,
          gameObject.scene.name,
          parentContractNames.Join(", "));

      return parentContainers;
    }

    private List<SceneDecoratorContext> LookupDecoratorContexts()
    {
      if (_contractNames.IsEmpty())
        return new List<SceneDecoratorContext>();

      return UnityUtil.AllLoadedScenes
        .Except(gameObject.scene)
        .SelectMany(scene => scene.GetRootGameObjects())
        .SelectMany(root => root.GetComponentsInChildren<SceneDecoratorContext>())
        .Where(decoratorContext => _contractNames.Contains(decoratorContext.DecoratedContractName))
        .ToList();
    }

    private void InstallBindings(List<MonoBehaviour> injectableMonoBehaviours)
    {
      _container.Bind(typeof(Context), typeof(SceneContext)).To<SceneContext>().FromInstance(this);
      _container.BindInterfacesTo<SceneContextRegistryAdderAndRemover>().AsSingle();

      // Add to registry first and remove from registry last
      _container.BindExecutionOrder<SceneContextRegistryAdderAndRemover>(-1);

      foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
        decoratorContext.InstallDecoratorSceneBindings();

      InstallSceneBindings(injectableMonoBehaviours);

      _container.Bind(typeof(SceneKernel), typeof(MonoKernel))
        .To<SceneKernel>().FromNewComponentOn(gameObject).AsSingle().NonLazy();

      _container.Bind<ZenjectSceneLoader>().AsSingle();

      if (ExtraBindingsInstallMethod != null)
      {
        ExtraBindingsInstallMethod(_container);
        // Reset extra bindings for next time we change scenes
        ExtraBindingsInstallMethod = null;
      }

      // Always install the installers last so they can be injected with
      // everything above
      foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
        decoratorContext.InstallDecoratorInstallers();

      InstallInstallers();

      foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
        decoratorContext.InstallLateDecoratorInstallers();

      if (ExtraBindingsLateInstallMethod != null)
      {
        ExtraBindingsLateInstallMethod(_container);
        // Reset extra bindings for next time we change scenes
        ExtraBindingsLateInstallMethod = null;
      }
    }
  }
}

#endif