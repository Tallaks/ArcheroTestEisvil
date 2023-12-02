using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers
{
  public class ParticlePrefabProvider : IParticlePrefabProvider
  {
    private readonly string[] _particles = { "Particles", "Gameplay" };
    private Dictionary<ParticleType, ParticleBehaviour> _prefabsByType;

    public async UniTask LoadParticlePrefabsAsync()
    {
      IList<GameObject> prefabs =
        await Addressables.LoadAssetsAsync<GameObject>(_particles as IEnumerable, null,
          Addressables.MergeMode.Intersection);
      _prefabsByType = prefabs
        .Select(prefab => prefab.GetComponent<ParticleBehaviour>())
        .ToDictionary(particle => particle.Type);
    }

    public ParticleBehaviour GetParticlePrefab(ParticleType particleType)
    {
      return _prefabsByType[particleType];
    }
  }
}