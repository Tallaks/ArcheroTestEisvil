using System.ComponentModel;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks.Factory
{
  public class HeroAttackHandlerFactory
  {
    private readonly IGameplayPrefabProvider _gameplayPrefabProvider;
    private readonly TransformContainer _transformContainer;
    private readonly ITargetPicker _targetPicker;
    private readonly IHeroAttackSystem _attackSystem;

    public HeroAttackHandlerFactory(
      IGameplayPrefabProvider gameplayPrefabProvider, IHeroAttackSystem attackSystem, ITargetPicker targetPicker,
      TransformContainer transformContainer)
    {
      _gameplayPrefabProvider = gameplayPrefabProvider;
      _attackSystem = attackSystem;
      _targetPicker = targetPicker;
      _transformContainer = transformContainer;
    }

    public IHeroAttackHandler Create(HeroConfig config, HeroBehaviour owner)
    {
      ArrowAttackHandler handler = config.DefaultAttack switch
      {
        HeroConfig.DefaultAttackType.Arrow => new ArrowAttackHandler(),
        HeroConfig.DefaultAttackType.None => throw new InvalidEnumArgumentException(),
        _ => throw new InvalidEnumArgumentException()
      };

      handler.Initialize(config.AttackDirection, owner, _attackSystem, _gameplayPrefabProvider, _targetPicker,
        _transformContainer);
      return handler;
    }
  }
}