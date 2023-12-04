using System.Linq;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public class HeroTargetPicker : ITargetPicker
  {
    private readonly IVisibilityService _visibilityService;
    private readonly ICharacterRegistry _characterRegistry;

    private EnemyBehaviour[] _allEnemies;
    private Vector3 _cachedClosestEnemyPosition;
    private float _savedTime;
    private EnemyBehaviour[] _visibleEnemies;

    public HeroTargetPicker(IVisibilityService visibilityService, ICharacterRegistry characterRegistry)
    {
      _characterRegistry = characterRegistry;
      _visibilityService = visibilityService;
    }

    public void Initialize()
    {
      _allEnemies = _characterRegistry.Enemies.ToArray();
      _visibleEnemies = new EnemyBehaviour[_allEnemies.Length];
      _savedTime = Time.time;
    }

    public Vector3 GetClosestTargetPosition(Vector3 fromPosition)
    {
      if (Time.time - _savedTime < 0.001f)
        return _cachedClosestEnemyPosition;
      _savedTime = Time.time;
      var visibleEnemiesCount = 0;
      var minDistance = float.MaxValue;
      for (var i = 0; i < _allEnemies.Length; i++)
      {
        EnemyBehaviour enemy = _allEnemies[i];
        if (!_visibilityService.EnemyIsVisibleByHero(_characterRegistry.Hero, enemy) || enemy.IsDead)
          continue;
        _visibleEnemies[visibleEnemiesCount] = enemy;
        visibleEnemiesCount++;
      }

      EnemyBehaviour closestVisibleEnemy = null;
      for (var i = 0; i < visibleEnemiesCount; i++)
      {
        if (_visibleEnemies[i].IsDead)
          continue;
        if (minDistance > Vector3.Distance(fromPosition, _visibleEnemies[i].Position))
        {
          minDistance = Vector3.Distance(fromPosition, _visibleEnemies[i].Position);
          closestVisibleEnemy = _visibleEnemies[i];
        }
      }

      if (closestVisibleEnemy != null)
      {
        _cachedClosestEnemyPosition = closestVisibleEnemy.Position.WithY(PhysicsConstants.ProjectileHeight);
        return _cachedClosestEnemyPosition;
      }

      minDistance = float.MaxValue;
      EnemyBehaviour closestEnemyAll = null;
      for (var i = 0; i < _allEnemies.Length; i++)
      {
        if (_allEnemies[i].IsDead)
          continue;
        if (minDistance > Vector3.Distance(fromPosition, _allEnemies[i].Position))
        {
          minDistance = Vector3.Distance(fromPosition, _allEnemies[i].Position);
          closestEnemyAll = _allEnemies[i];
        }
      }

      _cachedClosestEnemyPosition = closestEnemyAll != null
        ? closestEnemyAll.Position.WithY(PhysicsConstants.ProjectileHeight)
        : Vector3.zero;
      return _cachedClosestEnemyPosition;
    }

    public void Dispose()
    {
      _allEnemies = null;
    }
  }
}