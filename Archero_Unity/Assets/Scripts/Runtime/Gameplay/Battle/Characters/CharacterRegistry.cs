namespace Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Characters
{
  public class CharacterRegistry : ICharacterRegistry
  {
    public HeroBehaviour Hero { get; private set; }

    public void RegisterHero(HeroBehaviour hero)
    {
      Hero = hero;
    }
  }
}