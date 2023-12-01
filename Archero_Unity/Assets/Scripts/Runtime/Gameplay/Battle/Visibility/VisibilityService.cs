using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility
{
  public class VisibilityService : IVisibilityService
  {
    public bool EnemyIsVisibleByHero(HeroBehaviour hero, EnemyBehaviour enemy)
    {
      Vector3 direction = enemy.transform.position - hero.transform.position;
      var ray = new Ray(hero.transform.position.WithY(PhysicsConstants.RaycastYLevel), direction);
      int layerMask = PhysicsConstants.EnemiesAndObstaclesLayerMask;

      if (!Physics.Raycast(ray, out RaycastHit hit, direction.magnitude, layerMask))
        return false;
      return hit.collider.gameObject == enemy.gameObject;
    }

    public bool HeroIsVisibleByEnemy(EnemyBehaviour enemy, HeroBehaviour hero, bool ignoreObstacles = false)
    {
      Vector3 direction = hero.transform.position - enemy.transform.position;
      var ray = new Ray(enemy.transform.position.WithY(PhysicsConstants.RaycastYLevel), direction);
      int layerMask = ignoreObstacles
        ? PhysicsConstants.HeroOnlyLayerMask
        : PhysicsConstants.HeroAndObstacleLayerMask;

      if (!Physics.Raycast(ray, out RaycastHit hit, direction.magnitude, layerMask))
        return false;
      return hit.collider.gameObject == hero.gameObject;
    }
  }
}