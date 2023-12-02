using System;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage.Factory
{
  public class DamageApplierFactory
  {
    public IDamageApplier Create(HeroConfig.DamageType damageType)
    {
      switch (damageType)
      {
        case HeroConfig.DamageType.Default:
          return new ValueDamageApplier();
        case HeroConfig.DamageType.None:
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
      }

      throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
    }
  }
}