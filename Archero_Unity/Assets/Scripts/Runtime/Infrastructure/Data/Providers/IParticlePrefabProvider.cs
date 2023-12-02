using Cysharp.Threading.Tasks;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers
{
  public interface IParticlePrefabProvider
  {
    UniTask LoadParticlePrefabsAsync();
    ParticleBehaviour GetParticlePrefab(ParticleType particleType);
  }
}