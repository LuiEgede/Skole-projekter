namespace Lesson_4;

using System;

public class SalariedEmployee : Employee
{
    public decimal BaseSalary { get; private set; }
    public decimal Bonus { get; private set; }

    public SalariedEmployee(
        string name,
        string employeeId,
        DateTime hireDate,
        decimal baseSalary,
        decimal bonus)
        : base(name, employeeId, hireDate)
    {
        BaseSalary = baseSalary;
        Bonus = bonus;
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary + Bonus;
    }

    public override string Description()
    {
        string description = base.Description();

        return $"{description} (fastløn + bonus)";
    }
}
