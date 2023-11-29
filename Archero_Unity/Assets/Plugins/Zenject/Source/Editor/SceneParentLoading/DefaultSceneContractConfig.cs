using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Zenject.Internal
{
  public class DefaultSceneContractConfig : ScriptableObject
  {
    [Serializable]
    public class ContractInfo
    {
      public string ContractName;
      public SceneAsset Scene;
    }

    public const string ResourcePath = "ZenjectDefaultSceneContractConfig";

    public List<ContractInfo> DefaultContracts;
  }
}