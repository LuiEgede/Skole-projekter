using System;

namespace Lesson04
{
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

        // Fælles metode for alle køretøjer.
        public void Start()
        {
            Console.WriteLine($"{Brand} {Model} starter motoren.");
        }

        // Fælles metode for alle køretøjer.
        public string Description()
        {
            return $"{Brand} {Model}, topfart {TopSpeedKmh} km/t";
        }

        // Alle konkrete køretøjer SKAL selv implementere denne.
        public abstract decimal CalculateAnnualTax();
    }


    public class Car : Vehicle, IUdlejelig
    {
        public int DoorCount { get; set; }

        public Car(string brand, string model, int topSpeedKmh, int doorCount)
            : base(brand, model, topSpeedKmh)
        {
            DoorCount = doorCount;
        }

        // Override af den abstrakte metode fra Vehicle.
        public override decimal CalculateAnnualTax()
        {
            return 2400m;
        }

        // Car's egen implementation af IUdlejelig.
        public decimal CalculateRentalPrice(int numberOfDays)
        {
            return numberOfDays * 500m;
        }

        public void OpenTrunk()
        {
            Console.WriteLine($"{Brand} {Model}: bagagerummet åbnes.");
        }
    }


    public class Motorcycle : Vehicle, IUdlejelig
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

        // Override af den abstrakte metode fra Vehicle.
        public override decimal CalculateAnnualTax()
        {
            return 1000m;
        }

        // Motorcycle's egen implementation af IUdlejelig.
        public decimal CalculateRentalPrice(int numberOfDays)
        {
            return numberOfDays * 300m;
        }
    }
}
