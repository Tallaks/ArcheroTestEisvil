using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Constants
{
  public static class PhysicsConstants
  {
    private const string HeroLayerName = "Hero";
    private const string ObstacleLayerName = "Obstacle";
    private const string BordersLayerName = "Borders";
    private const string EnemyLayerName = "Enemy";
    private const string FlyingEnemyLayerName = "FlyingEnemy";
    private const string HitBoxLayerName = "Hitbox";

    public const float RaycastYLevel = 0.25f;
    public const float LineRenderingYLevel = 0.05f;
    public const float ProjectileHeight = 0.5f;

    public static readonly int EnemiesAndObstaclesLayerMask =
      LayerMask.GetMask(EnemyLayerName, FlyingEnemyLayerName, ObstacleLayerName, BordersLayerName);

    public static readonly int HeroOnlyLayerMask = LayerMask.GetMask(HeroLayerName);

    public static readonly int HeroAndObstacleLayerMask =
      LayerMask.GetMask(HeroLayerName, ObstacleLayerName, BordersLayerName);

    public static readonly int HitBoxAndObstacleLayerMask = LayerMask.GetMask(HitBoxLayerName, ObstacleLayerName);
  }
}