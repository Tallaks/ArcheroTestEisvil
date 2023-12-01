namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public interface ICharacterRegistry
  {
    HeroBehaviour Hero { get; }
    void RegisterHero(HeroBehaviour hero);
  }
}