namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage
{
  public interface IDamageApplier
  {
    void ApplyDamage(Health health, int baseDamage);
  }
}