using System;

namespace Zenject
{
  [NoReflectionBaking]
  public class ValidationMarker
  {
    public bool InstantiateFailed { get; private set; }

    public Type MarkedType { get; private set; }

    public ValidationMarker(
      Type markedType, bool instantiateFailed)
    {
      MarkedType = markedType;
      InstantiateFailed = instantiateFailed;
    }

    public ValidationMarker(Type markedType)
      : this(markedType, false)
    {
    }
  }
}