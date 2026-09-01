namespace Lesson_4;

using System;

public class HourlyEmployee : Employee
{
    public decimal HourlyRate { get; private set; }
    public double HoursWorked { get; private set; }

    public HourlyEmployee(
        string name,
        string employeeId,
        DateTime hireDate,
        decimal hourlyRate,
        double hoursWorked)
        : base(name, employeeId, hireDate)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public override decimal CalculateSalary()
    {
        return HourlyRate * (decimal)HoursWorked;
    }

    public override string Description()
    {
        string description = base.Description();

        return $"{description} ({HoursWorked} timer)";
    }
}
