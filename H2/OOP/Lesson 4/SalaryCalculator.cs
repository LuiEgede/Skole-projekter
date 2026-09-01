namespace Lesson_4;
public class SalaryCalculator
{
    public decimal CalculateBonus(decimal baseSalary)
    {
        // Standardbonus på 5 %
        return baseSalary * 0.05m;
    }

    public decimal CalculateBonus(decimal baseSalary, decimal percentage)
    {
        // Bonus ud fra en angivet procentsats
        return baseSalary * (percentage / 100);
    }

    public decimal CalculateBonus(
        decimal baseSalary,
        decimal percentage,
        int yearsOfSeniority)
    {
        // 1 % ekstra pr. anciennitetsår
        decimal totalPercentage = percentage + yearsOfSeniority;

        return baseSalary * (totalPercentage / 100);
    }
}
