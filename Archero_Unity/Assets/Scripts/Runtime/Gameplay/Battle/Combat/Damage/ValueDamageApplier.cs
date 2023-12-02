namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage
{
  public class ValueDamageApplier : IDamageApplier
  {
    public void ApplyDamage(Health health, int baseDamage)
    {
      health.Current -= baseDamage;
    }
  }
}