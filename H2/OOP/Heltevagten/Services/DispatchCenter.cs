using Heltevagten.Exceptions;
using Heltevagten.interfaces;
using Heltevagten.Models;

namespace Heltevagten.Services;

public class DispatchCenter
{
    private readonly IDispatchStrategy _dispatchStrategy;
    private readonly List<Hero> _heroes;
    private readonly List<Incident> _incidents;

    public DispatchCenter(IDispatchStrategy dispatchStrategy)
    {
        _dispatchStrategy = dispatchStrategy;
        _heroes = new List<Hero>();
        _incidents = new List<Incident>();
    }

    public IReadOnlyList<Hero> Heroes => _heroes.AsReadOnly();

    public IReadOnlyList<Incident> Incidents => _incidents.AsReadOnly();

    public void RegisterHero(Hero hero)
    {
        _heroes.Add(hero);
    }

    public void ReportIncident(Incident incident)
    {
        _incidents.Add(incident);
    }

    public string AssignHeroToIncident(Incident incident)
    {
        if (incident == null)
        {
            throw new ArgumentNullException(nameof(incident));
        }

        if (incident.IsResolved)
        {
            throw new InvalidOperationException("This incident is already resolved.");
        }

        var availableHeroes = _heroes.Where(hero => hero.IsAvailable).ToList();

        if (availableHeroes.Count == 0)
        {
            throw new NoSuitableHeroFoundException("No available heroes were found.");
        }

        var selectedHero = _dispatchStrategy.SelectHero(incident, availableHeroes);

        if (selectedHero == null)
        {
            throw new NoSuitableHeroFoundException("The dispatch strategy did not return a suitable hero.");
        }

        if (!selectedHero.IsAvailable)
        {
            throw new HeroUnavailableException($"{selectedHero.Name} is currently unavailable.");
        }

        selectedHero.MarkAsUnavailable();

        return $"{selectedHero.Name} has been assigned to incident at {incident.Location}.";
    }
}