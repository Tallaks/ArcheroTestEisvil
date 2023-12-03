using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Enemies;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.EnemyAttacks
{
  public class EnemyArcherAttackHandler : EnemyAimedAttackHandler
  {
    private EnemyArrowPool _arrowPool;
    private static EnemyArrowBehaviour _arrowPrefab;
    private static Transform _arrowContainer;

    public override EnemyAttackHandlerBase FinishInitialization()
    {
      Cooldown = Owner.Movement.MaxDistanceMovedByState / Owner.Movement.Speed;
      _arrowPool ??= new EnemyArrowPool(CreateArrow, GetArrow, ReleaseArrow, DestroyArrow);
      _arrowContainer ??= TransformContainer.EnemyProjectileContainer;
      _arrowPrefab ??= GameplayPrefabProvider.GetEnemyProjectilePrefab<EnemyArrowBehaviour>();
      return base.FinishInitialization();
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
      arrow.Reinitialize(Owner, _arrowPool, VisualEffectPerformer);
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