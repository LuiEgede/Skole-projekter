// ============================================================================
// Eksempel 3: Method Overloading
// ============================================================================
// Denne klasse bruges til at vise forskellen på overload og override.
// Den har ikke noget med Vehicle/Car/Motorcycle-opgaven at gøre.
// ============================================================================

using System;

namespace Lesson04
{
    public class Calculator
    {
        // Overload 1: to heltal
        public int Add(int a, int b)
        {
            Console.WriteLine("(bruger overload: int, int)");
            return a + b;
        }

        // Overload 2: to decimaltal
        public double Add(double a, double b)
        {
            Console.WriteLine("(bruger overload: double, double)");
            return a + b;
        }

        // Overload 3: tre heltal
        public int Add(int a, int b, int c)
        {
            Console.WriteLine("(bruger overload: int, int, int)");
            return a + b + c;
        }

        // Overload-eksempel
        public decimal CalculateBonus(decimal baseSalary)
        {
            return baseSalary * 0.05m;
        }

        public decimal CalculateBonus(
            decimal baseSalary,
            decimal percentage)
        {
            return baseSalary * (percentage / 100m);
        }

        public decimal CalculateBonus(
            decimal baseSalary,
            decimal percentage,
            int yearsOfSeniority)
        {
            decimal baseBonus =
                baseSalary * (percentage / 100m);

            decimal seniorityBonus =
                baseSalary * (yearsOfSeniority * 0.01m);

            return baseBonus + seniorityBonus;
        }
    }
}
