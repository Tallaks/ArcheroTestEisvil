using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn
{
  public abstract class SpawnPointBase : MonoBehaviour
  {
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
  }
}