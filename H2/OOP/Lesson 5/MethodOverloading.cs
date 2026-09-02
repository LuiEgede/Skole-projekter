// ============================================================================
// Eksempel 3: Overload - IKKE det samme som override!
// ============================================================================
// Formål: Vise method overloading tydeligt adskilt fra override (eksempel 2).
//
// VIGTIGT: Denne fil bruger INGEN arv overhovedet. "Calculator"-klassen har
// ingen basisklasse. Overload handler udelukkende om at have FLERE metoder
// med SAMME navn, men FORSKELLIG parameterliste, i den SAMME klasse.
//
// Kør filen ved at kopiere indholdet ind i din Program.cs.
// ============================================================================

using System;

namespace Lesson04.Example03
{
    // Bemærk: "Calculator" arver ikke fra noget (ingen ": Basisklasse").
    // Overload har intet med arv/basisklasser at gøre.
    public class Calculator
    {
        // Overload 1: to heltal
        public int Add(int a, int b)
        {
            Console.WriteLine("(bruger overload: int, int)");
            return a + b;
        }

        // Overload 2: to decimaltal
        // Samme metodenavn "Add", men PARAMETERTYPERNE er forskellige
        // (double i stedet for int). Dette er lovligt og er overloading.
        public double Add(double a, double b)
        {
            Console.WriteLine("(bruger overload: double, double)");
            return a + b;
        }

        // Overload 3: tre heltal
        // Samme metodenavn "Add", men ANTALLET af parametre er forskelligt.
        // Dette er også overloading.
        public int Add(int a, int b, int c)
        {
            Console.WriteLine("(bruger overload: int, int, int)");
            return a + b + c;
        }

        // Overload-eksempel, der minder mere om en rigtig opgave:
        // Beregn bonus med stigende grad af detaljering.
        public decimal CalculateBonus(decimal baseSalary)
        {
            // Standard: 5% af grundlønnen
            return baseSalary * 0.05m;
        }

        public decimal CalculateBonus(decimal baseSalary, decimal percentage)
        {
            return baseSalary * (percentage / 100m);
        }

        public decimal CalculateBonus(decimal baseSalary, decimal percentage, int yearsOfSeniority)
        {
            decimal baseBonus = baseSalary * (percentage / 100m);
            decimal seniorityBonus = baseSalary * (yearsOfSeniority * 0.01m);
            return baseBonus + seniorityBonus;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            // Compileren afgør allerede VED KOMPILERING, hvilken "Add"
            // der skal kaldes - ud fra antal og type af argumenter, du
            // sender med. Der er intet at gøre med objektets type ved
            // kørsel (modsat override/polymorfi i eksempel 2).
            Console.WriteLine(calculator.Add(2, 3)); // vælger (int, int)      -> 5
            Console.WriteLine(calculator.Add(2.5, 3.5)); // vælger (double, double) -> 6.0
            Console.WriteLine(calculator.Add(1, 2, 3)); // vælger (int, int, int) -> 6

            Console.WriteLine();

            decimal baseSalary = 30000m;
            Console.WriteLine($"Standardbonus: {calculator.CalculateBonus(baseSalary)} kr.");
            Console.WriteLine($"Bonus med 10%: {calculator.CalculateBonus(baseSalary, 10m)} kr.");
            Console.WriteLine(
                $"Bonus med 10% og 5 års anciennitet: " +
                $"{calculator.CalculateBonus(baseSalary, 10m, 5)} kr.");

            // ------------------------------------------------------------------
            // OPSUMMERING: Override (eksempel 2) vs. Overload (denne fil)
            // ------------------------------------------------------------------
            // Override:
            //   - Kræver arv (basisklasse + afledt klasse).
            //   - SAMME signatur (navn + parametre) som basisklassens metode.
            //   - Kræver "virtual" i basisklassen og "override" i den afledte klasse.
            //   - Hvilken version der køres, afgøres VED KØRSEL ud fra objektets
            //     faktiske type (polymorfi).
            //
            // Overload:
            //   - Kræver INGEN arv - kan ske i en helt almindelig klasse.
            //   - SAMME navn, men FORSKELLIG parameterliste (antal og/eller typer).
            //   - Kræver ingen særlige nøgleord.
            //   - Hvilken version der køres, afgøres VED KOMPILERING ud fra de
            //     argumenter, du sender med i kaldet.
            // ------------------------------------------------------------------
        }
    }
}
