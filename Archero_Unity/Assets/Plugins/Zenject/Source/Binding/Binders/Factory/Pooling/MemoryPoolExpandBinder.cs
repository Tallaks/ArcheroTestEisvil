namespace Zenject
{
  [NoReflectionBaking]
  public class MemoryPoolExpandBinder<TContract> : FactoryArgumentsToChoiceBinder<TContract>
  {
    protected MemoryPoolBindInfo MemoryPoolBindInfo { get; }

    public MemoryPoolExpandBinder(
      DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, MemoryPoolBindInfo poolBindInfo)
      : base(bindContainer, bindInfo, factoryBindInfo)
    {
      MemoryPoolBindInfo = poolBindInfo;

      ExpandByOneAtATime();
    }

    public FactoryArgumentsToChoiceBinder<TContract> ExpandByOneAtATime()
    {
      MemoryPoolBindInfo.ExpandMethod = PoolExpandMethods.OneAtATime;
      return this;
    }

    public FactoryArgumentsToChoiceBinder<TContract> ExpandByDoubling()
    {
      MemoryPoolBindInfo.ExpandMethod = PoolExpandMethods.Double;
      return this;
    }
  }
}