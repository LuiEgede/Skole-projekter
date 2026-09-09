using Heltevagten.Interfaces;
using Heltevagten.Models;

namespace Heltevagten.HeroTypes;

public class StrongHero : Hero, ISuperStrong
{
    public StrongHero(string name, int energy) : base(name, energy)
    {
    }

    public override string UseSignatureMove()
    {
        UseEnergy(15);
        return $"{Name} smashes through obstacles with super strength.";
    }

    public string LiftHeavyObject()
    {
        UseEnergy(10);
        return $"{Name} lifts the heavy object with incredible strength.";
    }
}