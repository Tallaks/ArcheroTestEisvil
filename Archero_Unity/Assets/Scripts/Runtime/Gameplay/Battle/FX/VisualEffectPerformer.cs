using System;
using System.Collections.Generic;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX
{
  public class VisualEffectPerformer : IVisualEffectPerformer
  {
    private readonly IParticlePrefabProvider _particlePrefabProvider;
    private readonly TransformContainer _transformContainer;
    private Dictionary<ParticleType, ObjectPool<ParticleBehaviour>> _poolsByType;

    public VisualEffectPerformer(IParticlePrefabProvider particlePrefabProvider, TransformContainer transformContainer)
    {
      _particlePrefabProvider = particlePrefabProvider;
      _transformContainer = transformContainer;
    }

    public void Initialize()
    {
      Debug.Log("Initializing VisualEffectPerformer");
      _poolsByType = new Dictionary<ParticleType, ObjectPool<ParticleBehaviour>>();
      foreach (ParticleType particleType in Enum.GetValues(typeof(ParticleType)))
        _poolsByType[particleType] = new ObjectPool<ParticleBehaviour>(
          () => CreatePrefab(particleType),
          null,
          ReleaseParticles
        );
    }

    public void Play(ParticleType particleType, Vector3 position)
    {
      ParticleBehaviour particle = _poolsByType[particleType].Get();
      particle.transform.position = position;
      particle.Play();
    }

    public void Dispose()
    {
      foreach (ObjectPool<ParticleBehaviour> pool in _poolsByType.Values)
        pool.Dispose();
    }

    private static void ReleaseParticles(ParticleBehaviour particleBehaviour)
    {
      particleBehaviour.transform.position = Vector3.down;
    }

    private ParticleBehaviour CreatePrefab(ParticleType particleType)
    {
      ParticleBehaviour particleBehaviour = Object.Instantiate(_particlePrefabProvider.GetParticlePrefab(particleType),
        _transformContainer.FxContainer);
      particleBehaviour.Initialize(_poolsByType[particleType]);
      return particleBehaviour;
    }
  }
}