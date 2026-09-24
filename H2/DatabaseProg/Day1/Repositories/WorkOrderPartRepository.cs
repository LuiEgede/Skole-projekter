using Dapper;
using Day1.Data;
using Day1.Interfaces;
using Day1.Models;

namespace Day1.Repositories;

public class WorkOrderPartRepository : IRepository<WorkOrderPart>
{
    public void Add(WorkOrderPart entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"INSERT INTO WorkOrderParts (WorkOrderId, PartId, Quantity)
                       VALUES (@WorkOrderId, @PartId, @Quantity);";

        connection.Execute(sql, entity);
    }

    public WorkOrderPart? GetById(int id)
    {
        throw new NotSupportedException(
            "WorkOrderPart uses a composite key and does not support single int ID lookup.");
    }

    public IEnumerable<WorkOrderPart> GetAll()
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM WorkOrderParts;";
        return connection.Query<WorkOrderPart>(sql);
    }

    public void Update(WorkOrderPart entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"UPDATE WorkOrderParts
                       SET Quantity = @Quantity
                       WHERE WorkOrderId = @WorkOrderId AND PartId = @PartId;";

        connection.Execute(sql, entity);
    }

    public void Delete(int id)
    {
        throw new NotSupportedException(
            "WorkOrderPart uses a composite key and does not support single int ID delete.");
    }
}