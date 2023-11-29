using System;

namespace Zenject
{
  [NoReflectionBaking]
  public class SignalDeclarationBindInfo
  {
    public object Identifier { get; set; }

    public Type SignalType { get; private set; }

    public bool RunAsync { get; set; }

    public int TickPriority { get; set; }

    public SignalMissingHandlerResponses MissingHandlerResponse { get; set; }

    public SignalDeclarationBindInfo(Type signalType) =>
      SignalType = signalType;
  }
}