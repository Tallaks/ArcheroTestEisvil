namespace Zenject
{
  [NoReflectionBaking]
  public class DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder : ScopeConcreteIdArgConditionCopyNonLazyBinder
  {
    protected SubContainerCreatorBindInfo SubContainerCreatorBindInfo { get; }

    public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(
      SubContainerCreatorBindInfo subContainerBindInfo, BindInfo bindInfo)
      : base(bindInfo) =>
      SubContainerCreatorBindInfo = subContainerBindInfo;

    public ScopeConcreteIdArgConditionCopyNonLazyBinder WithDefaultGameObjectParent(string defaultParentName)
    {
      SubContainerCreatorBindInfo.DefaultParentName = defaultParentName;
      return this;
    }
  }
}