using System;
using System.Collections.Generic;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public interface ICharacterRegistry : IDisposable
  {
    event Action OnAllEnemiesDead;
    HeroBehaviour Hero { get; }
    IEnumerable<EnemyBehaviour> Enemies { get; }
    void RegisterHero(HeroBehaviour hero);
    void RegisterEnemy(EnemyBehaviour enemyBehaviour);
    void RegisterEnemyDeath();
  }
}