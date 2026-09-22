namespace Day1.Models;

public class Part
{
    public int PartId { get; set; }
    public string PartName { get; set; } = null!;
    public decimal Price { get; set; }

    public List<WorkOrderPart> WorkOrderParts { get; set; } = new();
}