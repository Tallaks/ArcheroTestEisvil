using System.Collections;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;
using UnityEngine.Pool;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX
{
  public class ParticleBehaviour : MonoBehaviour
  {
    [field: SerializeField] public ParticleSystem MainParticleSystem { get; private set; }
    [field: SerializeField] public ParticleType Type { get; private set; }
    private ObjectPool<ParticleBehaviour> _objectPool;
    private WaitForSeconds _waitInstruction;

    public void Initialize(ObjectPool<ParticleBehaviour> objectPool)
    {
      _objectPool = objectPool;
      _waitInstruction = new WaitForSeconds(MainParticleSystem.main.duration);
    }

    public void Play()
    {
      MainParticleSystem.Play();
      StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
      yield return _waitInstruction;
      _objectPool.Release(this);
    }
  }
}