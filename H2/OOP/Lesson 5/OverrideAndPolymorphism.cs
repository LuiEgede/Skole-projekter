// ============================================================================
// Eksempel 2: virtual/override og polymorfi
// ============================================================================
// Formål: Vise hvordan "virtual" (i basisklassen) og "override" (i de afledte
// klasser) giver hver klasse sin egen version af en metode - og hvordan en
// liste af BASISKLASSE-referencer (List<Vehicle>) alligevel kalder den
// rigtige, SPECIFIKKE version for hvert objekt. Det er polymorfi i praksis.
//
// Kør filen ved at kopiere indholdet ind i din Program.cs.
// ============================================================================

using System;
using System.Collections.Generic;

namespace Lesson04.Example02
{
    public class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int TopSpeedKmh { get; set; }

        public Vehicle(string brand, string model, int topSpeedKmh)
        {
            Brand = brand;
            Model = model;
            TopSpeedKmh = topSpeedKmh;
        }

        public void Start()
        {
            Console.WriteLine($"{Brand} {Model} starter motoren.");
        }

        // "virtual" betyder: afledte klasser MÅ gerne give deres egen
        // implementering af denne metode via "override".
        public virtual string Description()
        {
            return $"{Brand} {Model}, topfart {TopSpeedKmh} km/t";
        }

        // En anden virtual-metode, der beregner en fiktiv årlig afgift.
        // Basisklassens version er en "fornuftig standard", som afledte
        // klasser kan vælge at overskrive, hvis de har brug for noget andet.
        public virtual decimal CalculateAnnualTax()
        {
            return 1000m;
        }
    }

    public class Car : Vehicle
    {
        public int DoorCount { get; set; }

        public Car(string brand, string model, int topSpeedKmh, int doorCount)
            : base(brand, model, topSpeedKmh)
        {
            DoorCount = doorCount;
        }

        // "override": samme signatur som Vehicle.Description() - dvs. samme
        // navn, samme returtype, samme parameterliste (her: ingen parametre).
        // Kun IMPLEMENTERINGEN er ny.
        public override string Description()
        {
            // base.Description() kalder basisklassens oprindelige version,
            // så vi kan BYGGE VIDERE på den i stedet for at gentage koden.
            return base.Description() + $", {DoorCount} døre";
        }

        public override decimal CalculateAnnualTax()
        {
            // Biler har en højere fiktiv afgift end standarden.
            return 2400m;
        }
    }

    public class Motorcycle : Vehicle
    {
        public bool RequiresHelmet { get; set; }

        public Motorcycle(string brand, string model, int topSpeedKmh, bool requiresHelmet)
            : base(brand, model, topSpeedKmh)
        {
            RequiresHelmet = requiresHelmet;
        }

        public override string Description()
        {
            string helmetInfo = RequiresHelmet ? "styrthjelm påkrævet" : "ingen hjelmkrav";
            return base.Description() + $", {helmetInfo}";
        }

        // Bemærk: Motorcycle overrider IKKE CalculateAnnualTax().
        // Det er helt lovligt - så bruges basisklassens standardversion (1000m)
        // automatisk, når CalculateAnnualTax() kaldes på et Motorcycle-objekt.
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Her er selve pointen med polymorfi:
            // Listens type er List<Vehicle> - altså basisklassen.
            // Men de objekter, vi lægger i den, er af de AFLEDTE typer.
            List<Vehicle> vehicles = new List<Vehicle>
            {
                new Car("Toyota", "Corolla", 180, 4),
                new Motorcycle("Honda", "CB500", 200, true),
                new Car("Tesla", "Model 3", 225, 4)
            };

            decimal totalTax = 0m;

            foreach (Vehicle vehicle in vehicles)
            {
                // "vehicle" er erklæret som Vehicle. Compileren ved kun, at
                // vehicle er "en eller anden Vehicle". Alligevel kaldes den
                // KORREKTE, specifikke Description()-metode for hvert objekts
                // FAKTISKE type (Car eller Motorcycle) - fordi metoden er
                // virtual/override.
                //
                // Dette afgøres ved KØRSEL (runtime), ikke ved kompilering.
                // Det er kernen i polymorfi: "samme kald, forskellig adfærd".
                Console.WriteLine(vehicle.Description());

                totalTax += vehicle.CalculateAnnualTax();
            }

            Console.WriteLine();
            Console.WriteLine($"Samlet årlig afgift for alle køretøjer: {totalTax} kr.");

            // Output for Description() bliver (typisk):
            //   Toyota Corolla, topfart 180 km/t, 4 døre
            //   Honda CB500, topfart 200 km/t, styrthjelm påkrævet
            //   Tesla Model 3, topfart 225 km/t, 4 døre
            //
            // Prøv selv: fjern "virtual" fra Vehicle.Description() og "override"
            // fra Car og Motorcycle (så metoderne bare er almindelige metoder med
            // samme navn). Koden vil IKKE kunne kompilere med "override"-ordet
            // tilbage - men hvis I fjerner override helt og lader Car/Motorcycle
            // definere en helt ny metode med samme navn (uden override), vil alle
            // tre linjer i stedet vise Vehicle's generiske beskrivelse. Det er
            // fordi polymorfi kræver virtual + override for at virke.
        }
    }
}
