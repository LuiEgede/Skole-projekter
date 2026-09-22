namespace Day1.Models;

public class WorkOrder
{
    public int WorkOrderId { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string WorkDescription { get; set; } = null!;
    public string WorkStatus { get; set; } = null!;

    public Car Car { get; set; } = null!;
    public List<WorkOrderPart> WorkOrderParts { get; set; } = new();
}