using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Movement
{
  [Serializable]
  public class LookAtPositionNode : MovementNodeBase
  {
    [SerializeField] private Transform _directionPoint;

    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      Movement.LookAt(_directionPoint.position);
      return true;
    }
  }
}