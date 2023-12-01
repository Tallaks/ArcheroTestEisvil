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
    private static ArrowBehaviourBase _arrowPrefab;
    private static Transform _arrowContainer;

    public float CooldownSec { get; private set; }
    private HeroConfig.DefaultAttackDirection _attackDirection;
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
      IGameplayPrefabProvider gameplayPrefabProvider, ITargetPicker targetPicker, TransformContainer transformContainer)
    {
      _targetPicker = targetPicker;
      CooldownSec = owner.BaseCooldownSec;
      _attackDirection = attackDirection;
      _owner = owner;
      _arrowPool ??= new ArrowPool(CreateArrow, GetArrow, ReleaseArrow, DestroyArrow);
      _arrowPrefab ??= gameplayPrefabProvider.GetHeroProjectilePrefab<ArrowBehaviourBase>();
      if (_arrowContainer is not null)
        return;
      _arrowContainer = new GameObject("ArrowContainer").transform;
      _arrowContainer.SetParent(transformContainer.HeroProjectileContainer);
    }

    public void Attack(Vector3 targetPosition)
    {
      ArrowBehaviourBase arrow = _arrowPool.Get();
      arrow.ShootAt(targetPosition, _attackDirection);
    }

    public void Dispose()
    {
      _arrowPool?.Dispose();
    }

    private static void ReleaseArrow(ArrowBehaviourBase arrow)
    {
      arrow.ReturnToPool();
    }

    private static void GetArrow(ArrowBehaviourBase arrow)
    {
      arrow.GetFromPool();
    }

    private ArrowBehaviourBase CreateArrow()
    {
      ArrowBehaviourBase arrowBehaviourBase =
        Object.Instantiate(_arrowPrefab, Vector3.down, Quaternion.identity, _arrowContainer);
      arrowBehaviourBase.Initialize(_owner, _arrowPool);
      return arrowBehaviourBase;
    }

    private static void DestroyArrow(ArrowBehaviourBase obj)
    {
      Object.Destroy(obj.gameObject);
    }
  }
}