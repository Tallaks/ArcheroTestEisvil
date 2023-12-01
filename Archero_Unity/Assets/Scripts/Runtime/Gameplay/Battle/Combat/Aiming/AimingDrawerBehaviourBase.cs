using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat.Aiming
{
  public abstract class AimingDrawerBehaviourBase : MonoBehaviour
  {
    public abstract void Initialize(EnemyBehaviour owner);
    public abstract void DrawAimingLine(Vector3 heroPosition, bool ignoreObstacles = false);
    public abstract void HideAimingLine();
  }
}