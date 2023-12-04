using System.Collections;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Constants;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Drop
{
  public abstract class ItemBehaviourBase : MonoBehaviour
  {
    [SerializeField] private GameObject _interactableObject;
    [SerializeField] private AnimationCurve _spawnAnimationCurve;
    [SerializeField] private float _moveToPlayerTime = 1f;
    private ICharacterRegistry _characterRegistry;
    private Coroutine _dropRoutine;

    private void OnDestroy()
    {
      StopAllCoroutines();
      _characterRegistry.OnAllEnemiesDead -= GetPickedUp;
      _characterRegistry.OnAllEnemiesDead -= MoveToPlayer;
    }

    public void Initialize(ICharacterRegistry characterRegistry)
    {
      _characterRegistry = characterRegistry;
      characterRegistry.OnAllEnemiesDead += GetPickedUp;
      characterRegistry.OnAllEnemiesDead += MoveToPlayer;
    }

    public void Drop(Vector3 position)
    {
      transform.position = position;
      Vector3 destination = position.RandomOnCircle();
      _dropRoutine = StartCoroutine(SpawnRoutine(destination));
    }

    protected abstract void GetPickedUp();

    private void MoveToPlayer()
    {
      _interactableObject.SetActive(true);
      if (_dropRoutine != null)
        StopCoroutine(_dropRoutine);
      StartCoroutine(MoveToPlayerRoutine());
    }

    private IEnumerator MoveToPlayerRoutine()
    {
      var currentTime = 0f;
      Vector3 startPosition = transform.position;
      while (currentTime < _moveToPlayerTime)
      {
        currentTime += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition,
          _characterRegistry.Hero.Position.WithY(PhysicsConstants.ProjectileHeight), currentTime / _moveToPlayerTime);
        yield return null;
      }
    }

    private IEnumerator SpawnRoutine(Vector3 destination)
    {
      float time = 0;
      float startPositionY = transform.position.y;
      Vector3 direction = (destination - transform.position).normalized;
      while (time < 1)
      {
        time += Time.deltaTime;
        transform.position =
          (transform.position + direction * Time.deltaTime).WithY(startPositionY + _spawnAnimationCurve.Evaluate(time));
        yield return null;
      }

      transform.position = destination;
    }
  }
}