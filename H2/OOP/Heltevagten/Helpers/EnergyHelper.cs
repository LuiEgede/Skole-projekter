using Heltevagten.Models;

namespace Heltevagten.Helpers;

public static class EnergyHelper
{
    public static int GetEnergyCost(SeverityLevel severity)
    {
        return severity switch
        {
            SeverityLevel.Low => 5,
            SeverityLevel.Medium => 10,
            SeverityLevel.High => 15,
            _ => throw new ArgumentOutOfRangeException(nameof(severity), "Unknown severity level.")
        };
    }
}