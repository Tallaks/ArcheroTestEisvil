using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class HeroBehaviour : DamageableBehaviour
  {
    [field: SerializeField] public HeroMovementBehaviour Movement { get; private set; }
    [field: SerializeField] public HitBox HitBox { get; private set; }
    public float BaseCooldownSec { get; private set; }
    public int BaseDamage { get; private set; }

    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    public bool IsMoving => Movement.IsMoving;

    public void Initialize(HeroConfig config, IInputService inputService, ITargetPicker targetPicker)
    {
      Movement.Initialize(this, inputService, targetPicker);
      Health = new Health(config.MaxHealth);
      Health.OnDead += Die;
      BaseDamage = config.BaseDamage;
      BaseCooldownSec = config.BaseCooldownSec;
      HitBox.Initialize(this);
    }

    public override void Die()
    {
      Movement.Dispose();
      Health.OnDead -= Die;
    }
  }
}