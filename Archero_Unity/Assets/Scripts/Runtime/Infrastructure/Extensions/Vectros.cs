using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Extensions
{
  public static class Vectros
  {
    public static Vector3 FromXYToXZ(this Vector2 vector)
    {
      return new Vector3(vector.x, 0, vector.y);
    }
  }
}