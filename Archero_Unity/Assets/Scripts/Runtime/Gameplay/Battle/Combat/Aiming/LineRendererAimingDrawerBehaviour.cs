using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Aiming
{
  public class LineRendererAimingDrawerBehaviour : AimingDrawerBehaviourBase
  {
    [SerializeField] private LineRenderer _lineRenderer;

    private EnemyBehaviour _owner;

    public override void Initialize(EnemyBehaviour owner)
    {
      _owner = owner;
    }

    public override void DrawAimingLine(Vector3 heroPosition, bool ignoreObstacles = false)
    {
      _lineRenderer.positionCount = 2;
      _lineRenderer.SetPosition(0, _owner.Position.WithY(PhysicsConstants.LineRenderingYLevel));
      _lineRenderer.SetPosition(1, heroPosition.WithY(PhysicsConstants.LineRenderingYLevel));
    }

    public override void HideAimingLine()
    {
      _lineRenderer.positionCount = 0;
    }

    public override void Dispose()
    {
      _lineRenderer.positionCount = 0;
    }
  }
}