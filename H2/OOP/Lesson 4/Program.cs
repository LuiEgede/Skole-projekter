namespace Lesson_4;

using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
     
        List<Employee> employees = new List<Employee>();

        employees.Add(
            new SalariedEmployee(
                "Anders Hansen",
                "EMP001",
                new DateTime(2022, 3, 1),
                32500m,
                2500m
            )
        );

        employees.Add(
            new SalariedEmployee(
                "Maria Jensen",
                "EMP002",
                new DateTime(2021, 8, 15),
                38000m,
                4000m
            )
        );

        employees.Add(
            new HourlyEmployee(
                "Peter Nielsen",
                "EMP003",
                new DateTime(2024, 1, 10),
                200m,
                160
            )
        );

        employees.Add(
            new HourlyEmployee(
                "Sofie Larsen",
                "EMP004",
                new DateTime(2023, 6, 1),
                225m,
                150
            )
        );

        decimal totalSalary = 0;

        Console.WriteLine("=== MEDARBEJDERE ===");
        Console.WriteLine();

       
        foreach (Employee employee in employees)
        {
            decimal salary = employee.CalculateSalary();

            Console.WriteLine(employee.Description());
            Console.WriteLine($"Medarbejder-ID: {employee.EmployeeId}");
            Console.WriteLine($"Ansat siden: {employee.HireDate:dd-MM-yyyy}");
            Console.WriteLine($"Løn: {salary:N2} kr.");
            Console.WriteLine();

            totalSalary += salary;
        }

        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Samlet lønsum: {totalSalary:N2} kr.");
        Console.WriteLine();



        Console.WriteLine("=== SALARY CALCULATOR ===");
        Console.WriteLine();

        SalaryCalculator calculator = new SalaryCalculator();

        decimal baseSalary = 30000m;

        // Overload med 1 parameter
        decimal bonus1 = calculator.CalculateBonus(baseSalary);

        // Overload med 2 parametre
        decimal bonus2 = calculator.CalculateBonus(
            baseSalary,
            10m
        );

        // Overload med 3 parametre
        decimal bonus3 = calculator.CalculateBonus(
            baseSalary,
            10m,
            5
        );

        Console.WriteLine(
            $"Standardbonus (5%): {bonus1:N2} kr."
        );

        Console.WriteLine(
            $"Bonus (10%): {bonus2:N2} kr."
        );

        Console.WriteLine(
            $"Bonus (10% + 5 års anciennitet): {bonus3:N2} kr."
        );
    }
}
