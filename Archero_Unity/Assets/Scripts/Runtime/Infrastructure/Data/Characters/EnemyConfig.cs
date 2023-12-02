using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters
{
  [CreateAssetMenu(fileName = "Enemy_Name", menuName = "ArcheroTest/Characters/EnemyConfig")]
  public class EnemyConfig : ScriptableObject
  {
    public string Id;
    public string Name;
    public EnemyBehaviour Prefab;
    public int MaxHealth;
    public int BaseDamage;

#if UNITY_EDITOR
    private void OnValidate()
    {
      if (string.IsNullOrEmpty(Id))
        Id = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)).ToString();
    }
#endif
  }
}