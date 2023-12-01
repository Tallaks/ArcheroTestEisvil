using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes
{
  [Serializable]
  public abstract class NodeBase
  {
    public string Name;
    public abstract void Initialize(NodeInitializationParams initializationParams);

    public virtual bool GetResult(float deltaTime, bool debug = false)
    {
      if (debug)
        Debug.Log($"{Name} | {GetType().Name} executing");
      return true;
    }
  }
}