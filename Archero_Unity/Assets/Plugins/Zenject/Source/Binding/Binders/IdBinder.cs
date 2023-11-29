namespace Zenject
{
  [NoReflectionBaking]
  public class IdBinder
  {
    private readonly BindInfo _bindInfo;

    public IdBinder(BindInfo bindInfo) =>
      _bindInfo = bindInfo;

    public void WithId(object identifier)
    {
      _bindInfo.Identifier = identifier;
    }
  }
}