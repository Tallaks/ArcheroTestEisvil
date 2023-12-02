using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
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
    public Vector3 Position => transform.position;
    public Quaternion Rotation => Movement.Rotation;
    public bool IsMoving => Movement.IsMoving;

    public void Initialize(HeroConfig config, IInputService inputService, ITargetPicker targetPicker)
    {
      Movement.Initialize(inputService, targetPicker);
      Health = new Health(config.MaxHealth);
      BaseDamage = config.BaseDamage;
      BaseCooldownSec = config.BaseCooldownSec;
      HitBox.Initialize(this);
    }
  }
}