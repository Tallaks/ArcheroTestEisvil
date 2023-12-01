using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility
{
  public interface IVisibilityService
  {
    bool EnemyIsVisibleByHero(HeroBehaviour hero, EnemyBehaviour enemy);
    bool HeroIsVisibleByEnemy(EnemyBehaviour enemy, HeroBehaviour hero, bool ignoreObstacles = false);
  }
}