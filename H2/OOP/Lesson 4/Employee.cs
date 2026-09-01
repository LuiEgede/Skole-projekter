namespace Lesson_4;

using System;

public class Employee
{
    public string Name { get; private set; }
    public string EmployeeId { get; private set; }
    public DateTime HireDate { get; private set; }

    public Employee(string name, string employeeId, DateTime hireDate)
    {
        Name = name;
        EmployeeId = employeeId;
        HireDate = hireDate;
    }

    public virtual decimal CalculateSalary()
    {
        
        return 0;
    }

    public virtual string Description()
    {
        return $"{Name} tjener {CalculateSalary():N2} kr. om måneden";
    }
}
