using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Constants
{
  public static class PhysicsConstants
  {
    private const string HeroLayerName = "Hero";
    private const string ObstacleLayerName = "Obstacle";
    private const string EnemyLayerName = "Enemy";

    public const float RaycastYLevel = 0.25f;
    public const float LineRenderingYLevel = 0.05f;
    public static readonly int EnemiesAndObstaclesLayerMask = LayerMask.GetMask(EnemyLayerName, ObstacleLayerName);
    public static readonly int HeroOnlyLayerMask = LayerMask.GetMask(HeroLayerName);
    public static readonly int HeroAndObstacleLayerMask = LayerMask.GetMask(HeroLayerName, ObstacleLayerName);
  }
}