using ModestTree;

namespace Zenject
{
  [ZenjectAllowDuringValidation]
  [NoReflectionBaking]
  public class LazyInject<T> : IValidatable
  {
    private readonly DiContainer _container;
    private readonly InjectContext _context;

    public T Value
    {
      get
      {
        if (!_hasValue)
        {
          _value = (T)_container.Resolve(_context);
          _hasValue = true;
        }

        return _value;
      }
    }

    private bool _hasValue;
    private T _value;

    public LazyInject(DiContainer container, InjectContext context)
    {
      Assert.DerivesFromOrEqual<T>(context.MemberType);

      _container = container;
      _context = context;
    }

    void IValidatable.Validate()
    {
      _container.Resolve(_context);
    }
  }
}