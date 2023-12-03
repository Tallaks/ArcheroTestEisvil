using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions
{
  public static class Vectros
  {
    public static Vector3 WithY(this Vector3 vector, float y)
    {
      return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 FromXYToXZ(this Vector2 vector)
    {
      return new Vector3(vector.x, 0, vector.y);
    }

    public static Vector3 RandomOnCircle(this Vector3 vector, float radius = 1f)
    {
      int angle = Random.Range(0, 360);
      return vector + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * radius;
    }
  }
}