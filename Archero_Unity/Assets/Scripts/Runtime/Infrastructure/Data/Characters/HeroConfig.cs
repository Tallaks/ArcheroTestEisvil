using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Characters
{
  [CreateAssetMenu(fileName = "Hero_Name", menuName = "ArcheroTest/Characters/HeroConfig")]
  public class HeroConfig : ScriptableObject
  {
    public string Id;
    public string Name;
    public HeroBehaviour Prefab;
    public int MaxHealth;

#if UNITY_EDITOR
    private void OnValidate()
    {
      if (string.IsNullOrEmpty(Id))
        Id = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)).ToString();
    }
#endif
  }
}