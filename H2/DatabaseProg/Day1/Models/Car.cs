namespace Day1.Models;

public class Car
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public string LicensePlate { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int ManufactureYear { get; set; }

    public Customer Customer { get; set; } = null!;
    public List<WorkOrder> WorkOrders { get; set; } = new();
}