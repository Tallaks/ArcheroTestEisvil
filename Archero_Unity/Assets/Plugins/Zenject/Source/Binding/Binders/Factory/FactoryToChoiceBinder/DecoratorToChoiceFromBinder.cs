namespace Zenject
{
  [NoReflectionBaking]
  public class DecoratorToChoiceFromBinder<TContract>
  {
    private readonly DiContainer _bindContainer;
    private readonly BindInfo _bindInfo;
    private readonly FactoryBindInfo _factoryBindInfo;

    public DecoratorToChoiceFromBinder(
      DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
    {
      _bindContainer = bindContainer;
      _bindInfo = bindInfo;
      _factoryBindInfo = factoryBindInfo;
    }

    public FactoryFromBinder<TContract, TConcrete> With<TConcrete>()
      where TConcrete : TContract
    {
      _bindInfo.ToChoice = ToChoices.Concrete;
      _bindInfo.ToTypes.Clear();
      _bindInfo.ToTypes.Add(typeof(TConcrete));

      return new FactoryFromBinder<TContract, TConcrete>(
        _bindContainer, _bindInfo, _factoryBindInfo);
    }
  }
}