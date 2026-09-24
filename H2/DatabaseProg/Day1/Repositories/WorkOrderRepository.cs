using System.Data;
using Dapper;
using Day1.Data;
using Day1.Interfaces;
using Day1.Models;

namespace Day1.Repositories;

public class WorkOrderRepository : IRepository<WorkOrder>
{
    public void Add(WorkOrder entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"INSERT INTO WorkOrders (CarId, StartDate, EndDate, WorkDescription, WorkStatus)
                       VALUES (@CarId, @StartDate, @EndDate, @WorkDescription, @WorkStatus);";

        connection.Execute(sql, entity);
    }

    public WorkOrder? GetById(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM WorkOrders WHERE WorkOrderId = @id;";
        return connection.QuerySingleOrDefault<WorkOrder>(sql, new { id });
    }

    public IEnumerable<WorkOrder> GetAll()
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM WorkOrders;";
        return connection.Query<WorkOrder>(sql);
    }

    public void Update(WorkOrder entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"UPDATE WorkOrders
                       SET CarId = @CarId,
                           StartDate = @StartDate,
                           EndDate = @EndDate,
                           WorkDescription = @WorkDescription,
                           WorkStatus = @WorkStatus
                       WHERE WorkOrderId = @WorkOrderId;";

        connection.Execute(sql, entity);
    }

    public void Delete(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "DELETE FROM WorkOrders WHERE WorkOrderId = @id;";
        connection.Execute(sql, new { id });
    }

    // Method to update the work order status using the stored procedure made in the databasee
    public void UpdateWorkOrderStatusWithProcedure(int workOrderId, string newStatus)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();

        var parameters = new
        {
            p_WorkOrderId = workOrderId,
            p_NewStatus = newStatus
        };

        connection.Execute(
            "UpdateWorkOrderStatus",
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}