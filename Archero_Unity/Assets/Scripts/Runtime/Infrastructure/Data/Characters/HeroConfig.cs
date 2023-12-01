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
    public enum DefaultAttackType
    {
      None = 0,
      Arrow = 1
    }

    public enum DefaultAttackDirection
    {
      None = 0,
      Forward = 1
    }

    public string Id;
    public string Name;
    public HeroBehaviour Prefab;
    public int MaxHealth;
    public DefaultAttackType DefaultAttack;
    public DefaultAttackDirection AttackDirection;
    public int BaseDamage;
    public float BaseCooldownSec;

#if UNITY_EDITOR
    private void OnValidate()
    {
      if (string.IsNullOrEmpty(Id))
        Id = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)).ToString();
    }
#endif
  }
}