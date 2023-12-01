namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public interface ICooldownAttackHandler
  {
    float CooldownSec { get; }
    void Update(float deltaTime);
  }
}