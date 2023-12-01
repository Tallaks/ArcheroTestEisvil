using System;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.HeroAttacks
{
  public interface ITargetPicker : IInitializable, IDisposable
  {
    Vector3 GetClosestTargetPosition(Vector3 fromPosition);
  }
}