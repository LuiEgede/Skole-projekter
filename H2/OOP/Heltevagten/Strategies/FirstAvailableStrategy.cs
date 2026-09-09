using Heltevagten.Exceptions;
using Heltevagten.interfaces;
using Heltevagten.Models;


namespace Heltevagten.Strategies;

public class FirstAvailableStrategy : IDispatchStrategy
{
    public Hero SelectHero(Incident incident, List<Hero> availableHeroes)
    {
        if (availableHeroes == null || availableHeroes.Count == 0)
        {
            throw new NoSuitableHeroFoundException("No available heroes were found.");
        }

        return availableHeroes[0];
    }
}