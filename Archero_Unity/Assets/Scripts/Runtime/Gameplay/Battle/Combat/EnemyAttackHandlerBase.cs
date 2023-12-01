using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Combat
{
  public abstract class EnemyAttackHandlerBase : MonoBehaviour
  {
    public float Cooldown { get; protected set; }
    public abstract void Initialize(EnemyBehaviour owner);
  }
}