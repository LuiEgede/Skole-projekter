using Day1.Interfaces;
using Day1.Models;

namespace Day1.Services;

public class WorkOrderService
{
    private readonly IRepository<WorkOrder> _repository;

    public WorkOrderService(IRepository<WorkOrder> repository)
    {
        _repository = repository;
    }

    public void CreateWorkOrder(int carId, string workDescription, string workStatus, string user)
    {
        var workOrder = new WorkOrder
        {
            CarId = carId,
            StartDate = DateTime.Today,
            EndDate = null,
            WorkDescription = workDescription,
            WorkStatus = workStatus
        };

        _repository.Add(workOrder);

        LogService.Log(
            user: user,
            operation: "CREATE",
            tableName: "WorkOrders",
            recordId: workOrder.WorkOrderId,
            oldData: null,
            newData:
            $"CarId={workOrder.CarId}, Status={workOrder.WorkStatus}, Description={workOrder.WorkDescription}");
    }

    public void UpdateWorkOrderStatus(int workOrderId, string newStatus, string user)
    {
        var workOrder = _repository.GetById(workOrderId);

        if (workOrder == null)
            return;

        string oldStatus = workOrder.WorkStatus;

        workOrder.WorkStatus = newStatus;
        _repository.Update(workOrder);

        LogService.Log(
            user: user,
            operation: "UPDATE",
            tableName: "WorkOrders",
            recordId: workOrder.WorkOrderId,
            oldData: $"Status={oldStatus}",
            newData: $"Status={newStatus}");
    }

    public void DeleteWorkOrder(int workOrderId, string user, bool isAdmin)
    {
        if (!isAdmin)
            return;

        var workOrder = _repository.GetById(workOrderId);

        if (workOrder == null)
            return;

        string oldData = $"Status={workOrder.WorkStatus}, Description={workOrder.WorkDescription}";

        _repository.Delete(workOrderId);

        LogService.Log(
            user: user,
            operation: "DELETE",
            tableName: "WorkOrders",
            recordId: workOrderId,
            oldData: oldData,
            newData: null);
    }

    public IEnumerable<WorkOrder> GetAllWorkOrders()
    {
        return _repository.GetAll();
    }
}