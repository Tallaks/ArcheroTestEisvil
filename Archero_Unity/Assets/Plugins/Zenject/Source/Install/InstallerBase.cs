namespace Zenject
{
  public abstract class InstallerBase : IInstaller
  {
    public virtual bool IsEnabled => true;

    protected DiContainer Container => _container;

    [Inject] private DiContainer _container;

    public abstract void InstallBindings();
  }
}