using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public class EnemyArcherAttackHandler : EnemyAimedAttackHandler
  {
    private EnemyArrowPool _arrowPool;
    private static EnemyArrowBehaviour _arrowPrefab;
    private static Transform _arrowContainer;
    private IVisualEffectPerformer _visualEffectPerformer;

    public override void Initialize(EnemyBehaviour owner,
      IGameplayPrefabProvider gameplayPrefabProvider,
      IVisualEffectPerformer visualEffectPerformer,
      TransformContainer transformContainer)
    {
      base.Initialize(owner, gameplayPrefabProvider, _visualEffectPerformer, transformContainer);
      _visualEffectPerformer = visualEffectPerformer;
      Cooldown = owner.Movement.MaxDistanceMovedByState / owner.Movement.Speed;
      _arrowPool ??= new EnemyArrowPool(CreateArrow, GetArrow, ReleaseArrow, DestroyArrow);
      _arrowContainer ??= transformContainer.EnemyProjectileContainer;
      _arrowPrefab ??= gameplayPrefabProvider.GetEnemyProjectilePrefab<EnemyArrowBehaviour>();
    }

    public override void Attack(Vector3 hero)
    {
      EnemyArrowBehaviour arrow = _arrowPool.Get();
      arrow.ShootAt(hero);
    }

    private static void DestroyArrow(EnemyArrowBehaviour arrow)
    {
      Destroy(arrow.gameObject);
    }

    private static void ReleaseArrow(EnemyArrowBehaviour arrow)
    {
      arrow.ReturnToPool();
    }

    private void GetArrow(EnemyArrowBehaviour arrow)
    {
      arrow.Reinitialize(Owner, _arrowPool, _visualEffectPerformer);
      arrow.GetFromPool();
    }

    private static EnemyArrowBehaviour CreateArrow()
    {
      EnemyArrowBehaviour newArrow = Instantiate(_arrowPrefab, Vector3.down, Quaternion.identity, _arrowContainer);
      return newArrow;
    }

    public override void Dispose()
    {
      AimingDrawer.Dispose();
    }
  }
}