using Day1.Data;
using Day1.Models;
using Microsoft.EntityFrameworkCore;

namespace Day1.Services;

public class WorkOrderService
{
    public void CreateWorkOrder(int carId, string workDescription, string workStatus, string user)
    {
        using var context = new AppDbContext();

        var workOrder = new WorkOrder
        {
            CarId = carId,
            StartDate = DateTime.Today,
            EndDate = null,
            WorkDescription = workDescription,
            WorkStatus = workStatus
        };

        context.WorkOrders.Add(workOrder);
        context.SaveChanges();

        LogService.Log(
            user: user,
            operation: "CREATE",
            tableName: "WorkOrders",
            recordId: workOrder.WorkOrderId,
            oldData: null,
            newData: $"Status={workOrder.WorkStatus}, Description={workOrder.WorkDescription}");
    }

    public void UpdateWorkOrderStatus(int workOrderId, string newStatus, string user)
    {
        using var context = new AppDbContext();

        var workOrder = context.WorkOrders.FirstOrDefault(w => w.WorkOrderId == workOrderId);

        if (workOrder == null)
            return;

        string oldStatus = workOrder.WorkStatus;

        workOrder.WorkStatus = newStatus;
        context.SaveChanges();

        LogService.Log(
            user: user,
            operation: "UPDATE",
            tableName: "WorkOrders",
            recordId: workOrder.WorkOrderId,
            oldData: $"Status={oldStatus}",
            newData: $"Status={newStatus}");
    }

    public void DeleteWorkOrder(int workOrderId, string user)
    {
        using var context = new AppDbContext();

        var workOrder = context.WorkOrders.FirstOrDefault(w => w.WorkOrderId == workOrderId);

        if (workOrder == null)
            return;

        string oldData = $"Status={workOrder.WorkStatus}, Description={workOrder.WorkDescription}";

        context.WorkOrders.Remove(workOrder);
        context.SaveChanges();

        LogService.Log(
            user: user,
            operation: "DELETE",
            tableName: "WorkOrders",
            recordId: workOrderId,
            oldData: oldData,
            newData: null);
    }
}