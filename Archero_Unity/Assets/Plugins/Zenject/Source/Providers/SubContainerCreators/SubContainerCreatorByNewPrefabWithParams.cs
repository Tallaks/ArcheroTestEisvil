#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;
using Object = UnityEngine.Object;

namespace Zenject
{
  [NoReflectionBaking]
  public class SubContainerCreatorByNewPrefabWithParams : ISubContainerCreator
  {
    private readonly IPrefabProvider _prefabProvider;
    private readonly Type _installerType;
    private readonly GameObjectCreationParameters _gameObjectBindInfo;

    protected DiContainer Container { get; }

    public SubContainerCreatorByNewPrefabWithParams(
      Type installerType, DiContainer container, IPrefabProvider prefabProvider,
      GameObjectCreationParameters gameObjectBindInfo)
    {
      _gameObjectBindInfo = gameObjectBindInfo;
      _prefabProvider = prefabProvider;
      Container = container;
      _installerType = installerType;
    }

    public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext,
      out Action injectAction)
    {
      Assert.That(!args.IsEmpty());

      Object prefab = _prefabProvider.GetPrefab(parentContext);
      DiContainer tempContainer = CreateTempContainer(args);

      bool shouldMakeActive;
      GameObject gameObject = tempContainer.CreateAndParentPrefab(
        prefab, _gameObjectBindInfo, null, out shouldMakeActive);

      var context = gameObject.GetComponent<GameObjectContext>();

      Assert.That(context != null,
        "Expected prefab with name '{0}' to container a component of type 'GameObjectContext'", prefab.name);

      context.Install(tempContainer);

      injectAction = () =>
      {
        // Note: We don't need to call ResolveRoots here because GameObjectContext does this for us
        tempContainer.Inject(context);

        if (shouldMakeActive && !Container.IsValidating)
        {
#if ZEN_INTERNAL_PROFILING
                    using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
          {
            gameObject.SetActive(true);
          }
        }
      };

      return context.Container;
    }

    private IEnumerable<InjectableInfo> GetAllInjectableIncludingBaseTypes()
    {
      InjectTypeInfo info = TypeAnalyzer.GetInfo(_installerType);

      while (info != null)
      {
        foreach (InjectableInfo injectable in info.AllInjectables)
          yield return injectable;

        info = info.BaseTypeInfo;
      }
    }

    private DiContainer CreateTempContainer(List<TypeValuePair> args)
    {
      DiContainer tempSubContainer = Container.CreateSubContainer();

      IEnumerable<InjectableInfo> allInjectables = GetAllInjectableIncludingBaseTypes();

      foreach (TypeValuePair argPair in args)
      {
        // We need to intelligently match on the exact parameters here to avoid the issue
        // brought up in github issue #217
        InjectableInfo match = allInjectables
          .Where(x => argPair.Type.DerivesFromOrEqual(x.MemberType))
          .OrderBy(x => ZenUtilInternal.GetInheritanceDelta(argPair.Type, x.MemberType)).FirstOrDefault();

        Assert.That(match != null,
          "Could not find match for argument type '{0}' when injecting into sub container installer '{1}'",
          argPair.Type, _installerType);

        tempSubContainer.Bind(match.MemberType)
          .FromInstance(argPair.Value).WhenInjectedInto(_installerType);
      }

      return tempSubContainer;
    }
  }
}

#endif