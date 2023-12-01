using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
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

    public Vector3 Position => transform.position;

    public void Initialize(HeroConfig config, IInputService inputService, ICharacterRegistry characterRegistry)
    {
      characterRegistry.RegisterHero(this);
      Movement.Initialize(inputService);
      Health = new Health(config.MaxHealth);
      HitBox.Initialize(this);
    }
  }
}