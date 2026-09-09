namespace Heltevagten.Models;

public class Incident
{
    public Incident(string description, string location, SeverityLevel severity)
    {
        Description = description;
        Location = location;
        Severity = severity;
        IsResolved = false;
    }

    public string Description { get; }

    public string Location { get; }

    public SeverityLevel Severity { get; }

    public bool IsResolved { get; private set; }

    public void MarkAsResolved()
    {
        IsResolved = true;
    }
}