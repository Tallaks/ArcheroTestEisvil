namespace Zenject
{
  [NoReflectionBaking]
  public class SignalTickPriorityCopyBinder : SignalCopyBinder
  {
    protected SignalDeclarationBindInfo SignalBindInfo { get; }

    public SignalTickPriorityCopyBinder(
      SignalDeclarationBindInfo signalBindInfo) =>
      SignalBindInfo = signalBindInfo;

    public SignalCopyBinder WithTickPriority(int priority)
    {
      SignalBindInfo.TickPriority = priority;
      SignalBindInfo.RunAsync = true;
      return this;
    }
  }
}