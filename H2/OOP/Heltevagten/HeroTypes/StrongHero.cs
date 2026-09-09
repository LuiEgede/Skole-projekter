namespace Heltevagten.HeroTypes;

public class StrongHero : Hero
{
    public StrongHero(string name, int energy) : base(name, energy)
    {
    }

    public override string UseSignatureMove()
    {
        UseEnergy(15);
        return $"{Name} smashes through obstacles with super strength.";
    }
}