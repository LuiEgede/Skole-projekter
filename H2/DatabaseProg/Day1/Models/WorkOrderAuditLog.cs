namespace Day1.Models;

public class WorkOrderAuditLog
{
    public int WorkOrderAuditLogId { get; set; }
    public int WorkOrderId { get; set; }
    public string? OldStatus { get; set; }
    public string? NewStatus { get; set; }
    public DateTime ChangedAt { get; set; }
}