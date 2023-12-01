using System.Collections.Generic;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class CharacterRegistry : ICharacterRegistry
  {
    private readonly List<EnemyBehaviour> _enemies = new();
    public HeroBehaviour Hero { get; private set; }

    public void RegisterHero(HeroBehaviour hero)
    {
      Hero = hero;
    }

    public void RegisterEnemy(EnemyBehaviour enemyBehaviour)
    {
      _enemies.Add(enemyBehaviour);
    }

    public void Dispose()
    {
      _enemies.Clear();
    }
  }
}