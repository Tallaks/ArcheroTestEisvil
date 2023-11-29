namespace Zenject
{
  [NoReflectionBaking]
  public class NullBindingFinalizer : IBindingFinalizer
  {
    public BindingInheritanceMethods BindingInheritanceMethod => BindingInheritanceMethods.None;

    public void FinalizeBinding(DiContainer container)
    {
      // Do nothing
    }
  }
}