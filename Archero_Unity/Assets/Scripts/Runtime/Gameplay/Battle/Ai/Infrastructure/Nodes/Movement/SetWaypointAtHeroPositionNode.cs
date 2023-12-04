using System;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai.Infrastructure.Nodes.Movement
{
  [Serializable]
  public class SetWaypointAtHeroPositionNode : MovementNodeBase
  {
    [SerializeField] private Transform _waypoint;
    private HeroBehaviour _hero;

    public override void Initialize(NodeInitializationParams initializationParams)
    {
      base.Initialize(initializationParams);
      _hero = initializationParams.Hero;
    }

    public override bool GetResult(float deltaTime, bool debug = false)
    {
      base.GetResult(deltaTime, debug);
      _waypoint.position = _hero.Position;
      return true;
    }
  }
}