using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Damage;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Movement;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Pause;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Services.Inputs;
using Tallaks.ArcheroTest.Runtime.UI.Gameplay.Battle;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class HeroBehaviour : DamageableBehaviour, IPauseHandler
  {
    [field: SerializeField] public HeroMovementBehaviour Movement { get; private set; }
    [field: SerializeField] public HitBox HitBox { get; private set; }
    [field: SerializeField] private HealthBarUi HpBar { get; set; }
    public float BaseCooldownSec { get; private set; }
    public int BaseDamage { get; private set; }

    public Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    public bool IsMoving => Movement.IsMoving;
    private IPauseService _pauseService;

    public void Initialize(HeroConfig config, IInputService inputService, IPauseService pauseService,
      ITargetPicker targetPicker)
    {
      _pauseService = pauseService;
      _pauseService.Register(this);
      Movement.Initialize(this, inputService, targetPicker);
      Health = new Health(config.MaxHealth);
      Health.OnDead += Die;
      HpBar.Initialize(this);
      BaseDamage = config.BaseDamage;
      BaseCooldownSec = config.BaseCooldownSec;
      HitBox.Initialize(this);
    }

    public void OnPause()
    {
      Movement.OnPause();
    }

    public void OnResume()
    {
      Movement.OnResume();
    }

    public override void Die()
    {
      _pauseService.Unregister(this);
      Movement.Dispose();
      Health.OnDead -= Die;
    }
  }
}