namespace Day1.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }

    public List<Car> Cars { get; set; } = new();
}