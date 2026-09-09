namespace Heltevagten;

public abstract class Hero
{
    private int _energy;

    protected Hero(string name, int energy)
    {
        Name = name;
        Energy = energy;
        IsAvailable = true;
    }

    public string Name { get; }

    public int Energy
    {
        get => _energy;
        private set => _energy = ValidateEnergy(value);
    }

    public bool IsAvailable { get; private set; }

    public void UseEnergy(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        Energy = Math.Max(0, Energy - amount);
    }

    public void RestoreEnergy(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        Energy = Math.Min(100, Energy + amount);
    }

    public abstract string UseSignatureMove();

    private static int ValidateEnergy(int value)
    {
        if (value < 0 || value > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Energy must be between 0 and 100.");
        }

        return value;
    }
}