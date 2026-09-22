namespace Day1.Models;

public class WorkOrderPart
{
    public int WorkOrderId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }

    public WorkOrder WorkOrder { get; set; } = null!;
    public Part Part { get; set; } = null!;
}