namespace Heltevagten.HeroTypes;

public class HealingHero : Hero
{
    public HealingHero(string name, int energy) : base(name, energy)
    {
    }

    public override string UseSignatureMove()
    {
        UseEnergy(10);
        return $"{Name} heals injured citizens and restores hope.";
    }
}