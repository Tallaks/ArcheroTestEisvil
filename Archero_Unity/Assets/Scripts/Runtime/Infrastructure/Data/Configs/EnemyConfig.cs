using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEditor;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Configs
{
  [CreateAssetMenu(fileName = "Enemy_Name", menuName = "ArcheroTest/Characters/EnemyConfig")]
  public class EnemyConfig : ScriptableObject
  {
    public string Id;
    public string Name;
    public EnemyBehaviour Prefab;
    public int MaxHealth;
    public int BaseDamage;
    public DroppableItems[] DroppedItems;

#if UNITY_EDITOR
    private void OnValidate()
    {
      if (string.IsNullOrEmpty(Id))
        Id = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)).ToString();
    }
#endif
  }
}