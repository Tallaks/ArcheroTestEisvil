using System;
using System.Diagnostics.CodeAnalysis;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes
{
  [Serializable]
  [SuppressMessage("ReSharper", "NotAccessedField.Global")]
  public abstract class NodeBase
  {
    public string Name;
    public abstract void Initialize(NodeInitializationParams initializationParams);
    public abstract bool GetResult(float deltaTime);
  }
}