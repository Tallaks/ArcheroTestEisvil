using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Visibility;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Ai
{
  public abstract class EnemyBrainBehaviourBase : MonoBehaviour
  {
    public abstract void Initialize(EnemyBehaviour owner, ICharacterRegistry characterRegistry,
      IVisibilityService visibilityService);

    public abstract void UpdateBehaviour(float deltaTime);
  }
}