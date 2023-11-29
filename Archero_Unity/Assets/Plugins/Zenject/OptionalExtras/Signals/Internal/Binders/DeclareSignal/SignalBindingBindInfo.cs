using System;

namespace Zenject
{
  [NoReflectionBaking]
  public class SignalBindingBindInfo
  {
    public object Identifier { get; set; }

    public Type SignalType { get; private set; }

    public SignalBindingBindInfo(Type signalType) =>
      SignalType = signalType;
  }
}