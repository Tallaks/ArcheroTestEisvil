using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Hero;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Projectiles.Pools;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public class ArrowAttackHandler : IHeroAttackHandler, ICooldownAttackHandler
  {
    private static ArrowPool _arrowPool;
    private static ArrowBehaviour _arrowPrefab;
    private static Transform _arrowContainer;

    public float CooldownSec { get; private set; }
    private HeroConfig.DefaultAttackDirection _attackDirection;
    private IHeroAttackSystem _attackSystem;
    private float _currentTime;
    private HeroBehaviour _owner;
    private ITargetPicker _targetPicker;

    public void Update(float deltaTime)
    {
      if (_owner.IsMoving)
      {
        _currentTime = 0f;
        return;
      }

      _currentTime += deltaTime;
      if (_currentTime < CooldownSec)
        return;
      _currentTime = 0f;
      Attack(_targetPicker.GetClosestTargetPosition(_owner.Position));
    }

    public void Initialize(HeroConfig.DefaultAttackDirection attackDirection, HeroBehaviour owner,
      IHeroAttackSystem attackSystem,
      IGameplayPrefabProvider gameplayPrefabProvider, ITargetPicker targetPicker, TransformContainer transformContainer)
    {
      _attackSystem = attackSystem;
      _targetPicker = targetPicker;
      CooldownSec = owner.BaseCooldownSec;
      _attackDirection = attackDirection;
      _owner = owner;
      _arrowPool ??= new ArrowPool(CreateArrow, GetArrow, ReleaseArrow, DestroyArrow);
      _arrowPrefab ??= gameplayPrefabProvider.GetHeroProjectilePrefab<ArrowBehaviour>();
      if (_arrowContainer is not null)
        return;
      _arrowContainer = new GameObject("ArrowContainer").transform;
      _arrowContainer.SetParent(transformContainer.HeroProjectileContainer);
    }

    public void Attack(Vector3 targetPosition)
    {
      ArrowBehaviour arrow = _arrowPool.Get();
      arrow.ShootAt(targetPosition, _attackDirection);
    }

    public void Dispose()
    {
      _arrowPool?.Dispose();
    }

    private static void ReleaseArrow(ArrowBehaviour arrow)
    {
      arrow.ReturnToPool();
    }

    private static void GetArrow(ArrowBehaviour arrow)
    {
      arrow.GetFromPool();
    }

    private ArrowBehaviour CreateArrow()
    {
      ArrowBehaviour arrowBehaviour =
        Object.Instantiate(_arrowPrefab, Vector3.down, Quaternion.identity, _arrowContainer);
      arrowBehaviour.Initialize(_owner, _arrowPool, _attackSystem);
      return arrowBehaviour;
    }

    private static void DestroyArrow(ArrowBehaviour obj)
    {
      Object.Destroy(obj.gameObject);
    }
  }
}