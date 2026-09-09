using Heltevagten.Models;

namespace Heltevagten.interfaces;

public interface IDispatchStrategy
{
    Hero SelectHero(Incident incident, List<Hero> availableHeroes);
}