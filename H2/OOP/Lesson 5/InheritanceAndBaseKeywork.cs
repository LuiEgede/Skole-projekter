// ============================================================================
// Trin 1: Abstrakt basisklasse, arv og base
// ============================================================================

using System;

namespace Lesson04
{
    // Vehicle er nu en ABSTRACT klasse.
    public abstract class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int TopSpeedKmh { get; set; }

        public Vehicle(string brand, string model, int topSpeedKmh)
        {
            Brand = brand;
            Model = model;
            TopSpeedKmh = topSpeedKmh;

            Console.WriteLine($"[Vehicle-konstruktør] Opretter {brand} {model}");
        }

        // Almindelig metode.
        // Denne kode er fælles for alle køretøjer.
        public void Start()
        {
            Console.WriteLine($"{Brand} {Model} starter motoren.");
        }

        // En anden almindelig metode, som alle køretøjer kan bruge.
        public string Description()
        {
            return $"{Brand} {Model}, topfart {TopSpeedKmh} km/t";
        }

        // Abstrakt metode.
        // Alle konkrete køretøjstyper SKAL lave deres egen implementation.
        public abstract decimal CalculateAnnualTax();
    }


    // =========================================================================
    // Car
    // =========================================================================

    public class Car : Vehicle
    {
        public int DoorCount { get; set; }

        public Car(string brand, string model, int topSpeedKmh, int doorCount)
            : base(brand, model, topSpeedKmh)
        {
            DoorCount = doorCount;

            Console.WriteLine($"[Car-konstruktør] Sætter DoorCount = {doorCount}");
        }

        // Car laver sin egen beregning af årsafgift.
        public override decimal CalculateAnnualTax()
        {
            return 2400m;
        }

        // En metode, der kun findes på Car.
        public void OpenTrunk()
        {
            Console.WriteLine($"{Brand} {Model}: bagagerummet åbnes.");
        }
    }


    // =========================================================================
    // Motorcycle
    // =========================================================================

    public class Motorcycle : Vehicle
    {
        public bool RequiresHelmet { get; set; }

        public Motorcycle(
            string brand,
            string model,
            int topSpeedKmh,
            bool requiresHelmet)
            : base(brand, model, topSpeedKmh)
        {
            RequiresHelmet = requiresHelmet;
        }

        // Motorcycle laver sin egen beregning af årsafgift.
        public override decimal CalculateAnnualTax()
        {
            return 1000m;
        }
    }
}
