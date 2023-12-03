using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks.Factory
{
  public interface IEnemyAttackHandlerBuilder
  {
    EnemyAttackHandlerBase Build(EnemyBehaviour owner, EnemyAttackHandlerBase attackHandler);
  }
}