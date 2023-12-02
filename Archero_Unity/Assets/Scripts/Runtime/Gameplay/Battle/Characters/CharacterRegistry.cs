using System;
using System.Collections.Generic;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class CharacterRegistry : ICharacterRegistry
  {
    private readonly List<EnemyBehaviour> _enemies = new();

    public event Action OnAllEnemiesDead;
    public HeroBehaviour Hero { get; private set; }
    public IEnumerable<EnemyBehaviour> Enemies => _enemies;
    private int _deadEnemiesCount;

    public void RegisterHero(HeroBehaviour hero)
    {
      Hero = hero;
    }

    public void RegisterEnemy(EnemyBehaviour enemyBehaviour)
    {
      _enemies.Add(enemyBehaviour);
    }

    public void RegisterEnemyDeath()
    {
      _deadEnemiesCount++;
      if (_deadEnemiesCount == _enemies.Count)
        OnAllEnemiesDead?.Invoke();
    }

    public void Dispose()
    {
      _enemies.Clear();
      Hero = null;
    }
  }
}