using Heltevagten.Helpers;
using Heltevagten.HeroTypes;
using Heltevagten.interfaces;
using Heltevagten.Models;
using Heltevagten.Services;
using Heltevagten.Strategies;

namespace Heltevagten;

internal class Program
{
    private static void Main(string[] args)
    {
        IDispatchStrategy strategy = new FirstAvailableStrategy();
        DispatchCenter dispatchCenter = new DispatchCenter(strategy);

        FlyingHero skyHero = new FlyingHero("Sky Falcon", 100);
        StrongHero muscleHero = new StrongHero("Titan Bolt", 100);
        HealingHero healerHero = new HealingHero("Luna Pulse", 100);

        dispatchCenter.RegisterHero(skyHero);
        dispatchCenter.RegisterHero(muscleHero);
        dispatchCenter.RegisterHero(healerHero);

        Incident incident1 = new Incident("A giant rubber duck blocks the harbor", "Harbor", SeverityLevel.Low);
        Incident incident2 = new Incident("Runaway alpacas on the highway", "Highway 1", SeverityLevel.Medium);
        Incident incident3 = new Incident("Out-of-control drone show above city hall", "City Hall", SeverityLevel.High);

        dispatchCenter.ReportIncident(incident1);
        dispatchCenter.ReportIncident(incident2);
        dispatchCenter.ReportIncident(incident3);

        Console.WriteLine("=== HERO DEMO ===");
        Console.WriteLine(skyHero.Fly());
        Console.WriteLine(muscleHero.LiftHeavyObject());
        Console.WriteLine(skyHero.UseSignatureMove());
        Console.WriteLine(muscleHero.UseSignatureMove());
        Console.WriteLine(healerHero.UseSignatureMove());

        Console.WriteLine();
        Console.WriteLine("=== DISPATCH DEMO ===");

        try
        {
            string assignmentResult = dispatchCenter.AssignHeroToIncident(incident1);
            Console.WriteLine(assignmentResult);

            Hero assignedHero = SearchHelper.FindFirst(dispatchCenter.Heroes.ToList(), hero => !hero.IsAvailable)!;

            dispatchCenter.ResolveIncident(incident1, assignedHero, ReleaseHeroAndRestoreEnergy);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("=== SECOND INCIDENT WITH LAMBDA CALLBACK ===");

        try
        {
            string assignmentResult = dispatchCenter.AssignHeroToIncident(incident2);
            Console.WriteLine(assignmentResult);

            Hero assignedHero = SearchHelper.FindFirst(dispatchCenter.Heroes.ToList(), hero => !hero.IsAvailable)!;

            dispatchCenter.ResolveIncident(incident2, assignedHero, (resolvedIncident, resolvedHero) =>
            {
                int energyCost = EnergyHelper.GetEnergyCost(resolvedIncident.Severity);
                resolvedHero.RestoreEnergy(energyCost);
                resolvedHero.MarkAsAvailable();

                Console.WriteLine(
                    $"{resolvedHero.Name} is back on duty after resolving: {resolvedIncident.Description}");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("=== INCIDENT LIST ===");

        foreach (Incident incident in dispatchCenter.Incidents)
        {
            Console.WriteLine(
                $"{incident.Description} | {incident.Location} | {incident.Severity} | Resolved: {incident.IsResolved}");
        }

        Console.WriteLine();
        Console.WriteLine("=== HERO STATUS ===");

        foreach (Hero hero in dispatchCenter.Heroes)
        {
            Console.WriteLine($"{hero.Name} | Energy: {hero.Energy} | Available: {hero.IsAvailable}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static void ReleaseHeroAndRestoreEnergy(Incident incident, Hero hero)
    {
        int energyCost = EnergyHelper.GetEnergyCost(incident.Severity);
        hero.RestoreEnergy(energyCost);
        hero.MarkAsAvailable();

        Console.WriteLine($"{hero.Name} is back on duty after resolving: {incident.Description}");
    }
}