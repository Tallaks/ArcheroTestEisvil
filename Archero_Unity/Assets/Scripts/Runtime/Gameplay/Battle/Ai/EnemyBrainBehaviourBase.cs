using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai
{
  public abstract class EnemyBrainBehaviourBase : MonoBehaviour
  {
    public abstract void Initialize(EnemyBehaviour owner);
    public abstract void UpdateBehaviour(float deltaTime);
  }
}