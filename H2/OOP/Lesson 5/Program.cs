using System;
using System.Collections.Generic;

namespace Lesson04
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ================================================================
            // TRIN 3 - Test af abstrakt klasse
            // ================================================================

            Console.WriteLine("=== Test af abstrakt klasse ===");
            Console.WriteLine();

            // Denne linje ville give en compilerfejl,
            // fordi Vehicle er abstract:
            //
            // Vehicle vehicle = new Vehicle("Toyota", "Corolla", 180);


            // ================================================================
            // TRIN 3 - Polymorfi via Vehicle
            // ================================================================

            Console.WriteLine("=== Polymorfi via Vehicle ===");

            List<Vehicle> vehicles = new List<Vehicle>
            {
                new Car("Toyota", "Corolla", 180, 4),
                new Motorcycle("Honda", "CBR900RR", 290, true),
                new Car("Tesla", "Model 3", 225, 4)
            };

            decimal totalTax = 0m;

            foreach (Vehicle vehicle in vehicles)
            {
                // Start() er en fælles metode fra Vehicle.
                vehicle.Start();

                // CalculateAnnualTax() er abstrakt i Vehicle.
                //
                // Derfor kaldes den rigtige override-metode:
                // Car → 2400 kr.
                // Motorcycle → 1000 kr.
                decimal tax = vehicle.CalculateAnnualTax();

                Console.WriteLine(vehicle.Description());
                Console.WriteLine($"Årsafgift: {tax} kr.");
                Console.WriteLine();

                totalTax += tax;
            }

            Console.WriteLine(
                $"Samlet årlig afgift: {totalTax} kr.");

            Console.WriteLine();


            // ================================================================
            // TRIN 3 - Polymorfi via interface
            // ================================================================

            Console.WriteLine("=== Polymorfi via IUdlejelig ===");

            // Listen bruger INTERFACET som type.
            // Både Car og Motorcycle kan være i listen,
            // fordi de begge implementerer IUdlejelig.
            List<IUdlejelig> rentableVehicles =
                new List<IUdlejelig>
                {
                    new Car("Toyota", "Corolla", 180, 4),
                    new Motorcycle("Honda", "CB500", 200, true),
                    new Car("Tesla", "Model 3", 225, 4)
                };

            int numberOfDays = 5;

            foreach (IUdlejelig vehicle in rentableVehicles)
            {
                // Her kaldes den individuelle implementation
                // af CalculateRentalPrice().
                decimal rentalPrice =
                    vehicle.CalculateRentalPrice(numberOfDays);

                Console.WriteLine(
                    $"Lejepris for {numberOfDays} dage: {rentalPrice} kr.");
            }
        }
    }
}
