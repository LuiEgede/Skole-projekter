using Heltevagten.Interfaces;

namespace Heltevagten.HeroTypes;

public class FlyingHero : Hero, IFlyable
{
    public FlyingHero(string name, int energy) : base(name, energy)
    {
    }

    public override string UseSignatureMove()
    {
        UseEnergy(10);
        return $"{Name} soars through the sky and rescues people from above.";
    }

    public string Fly()
    {
        UseEnergy(15);
        return $"{Name} flies swiftly to the incident.";
    }
}