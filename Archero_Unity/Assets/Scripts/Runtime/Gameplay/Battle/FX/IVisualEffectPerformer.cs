using System;
using Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.FX
{
  public interface IVisualEffectPerformer : IInitializable, IDisposable
  {
    void Play(ParticleType particleType, Vector3 position);
  }
}