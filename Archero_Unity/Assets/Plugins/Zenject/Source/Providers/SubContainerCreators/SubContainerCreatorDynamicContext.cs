#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
  [NoReflectionBaking]
  public abstract class SubContainerCreatorDynamicContext : ISubContainerCreator
  {
    protected DiContainer Container { get; }

    public SubContainerCreatorDynamicContext(DiContainer container) =>
      Container = container;

    public DiContainer CreateSubContainer(
      List<TypeValuePair> args, InjectContext parentContext, out Action injectAction)
    {
      bool shouldMakeActive;
      GameObject gameObj = CreateGameObject(parentContext, out shouldMakeActive);

      var context = gameObj.AddComponent<GameObjectContext>();

      AddInstallers(args, context);

      context.Install(Container);

      injectAction = () =>
      {
        // Note: We don't need to call ResolveRoots here because GameObjectContext does this for us
        Container.Inject(context);

        if (shouldMakeActive && !Container.IsValidating)
        {
#if ZEN_INTERNAL_PROFILING
                    using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
          {
            gameObj.SetActive(true);
          }
        }
      };

      return context.Container;
    }

    protected abstract void AddInstallers(List<TypeValuePair> args, GameObjectContext context);
    protected abstract GameObject CreateGameObject(InjectContext context, out bool shouldMakeActive);
  }
}

#endif