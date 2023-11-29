using System;
using System.Collections.Generic;

namespace Zenject
{
  [NoReflectionBaking]
  public class FactoryBindInfo
  {
    public Type FactoryType { get; private set; }

    public Func<DiContainer, IProvider> ProviderFunc { get; set; }

    public List<TypeValuePair> Arguments { get; set; }

    public FactoryBindInfo(Type factoryType)
    {
      FactoryType = factoryType;
      Arguments = new List<TypeValuePair>();
    }
  }
}