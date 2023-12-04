using System;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Movement
{
  [Serializable]
  public class MovementToWaypointNode : MovementNodeBase
  {
    [SerializeField] private Transform _waypoint;

    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      Vector3 waypointPosition = _waypoint.position;
      Movement.MoveTo(waypointPosition);
      _waypoint.position = waypointPosition;
      return true;
    }
  }
}