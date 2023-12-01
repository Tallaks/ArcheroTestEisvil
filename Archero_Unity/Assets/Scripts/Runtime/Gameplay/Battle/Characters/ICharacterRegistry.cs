using System;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public interface ICharacterRegistry : IDisposable
  {
    HeroBehaviour Hero { get; }
    void RegisterHero(HeroBehaviour hero);
    void RegisterEnemy(EnemyBehaviour enemyBehaviour);
  }
}