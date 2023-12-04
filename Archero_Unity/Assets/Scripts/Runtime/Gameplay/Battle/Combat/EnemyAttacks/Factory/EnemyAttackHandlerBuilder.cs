using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks.Factory
{
  public class EnemyAttackHandlerBuilder : IEnemyAttackHandlerBuilder
  {
    private readonly IGameplayPrefabProvider _gameplayPrefabProvider;
    private readonly IVisualEffectPerformer _visualEffectPerformer;
    private readonly TransformContainer _transformContainer;

    public EnemyAttackHandlerBuilder(IGameplayPrefabProvider gameplayPrefabProvider,
      IVisualEffectPerformer visualEffectPerformer, TransformContainer transformContainer)
    {
      _gameplayPrefabProvider = gameplayPrefabProvider;
      _visualEffectPerformer = visualEffectPerformer;
      _transformContainer = transformContainer;
    }

    public EnemyAttackHandlerBase Build(EnemyBehaviour owner, EnemyAttackHandlerBase attackHandler)
    {
      attackHandler.StartInitialization(owner);
      switch (attackHandler)
      {
        case EnemyArcherAttackHandler archerAttackHandler:
          return BuildArcherAttackHandler(archerAttackHandler).FinishInitialization();
        case EnemyCollisionAttackHandler collisionAttackHandler:
          return collisionAttackHandler.FinishInitialization();
      }

      return null;
    }

    private EnemyAttackHandlerBase BuildArcherAttackHandler(EnemyAttackHandlerBase archerAttackHandler)
    {
      return archerAttackHandler.WithProjectiles(_gameplayPrefabProvider, _visualEffectPerformer, _transformContainer);
    }
  }
}