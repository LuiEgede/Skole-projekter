using Dapper;
using Day1.Data;
using Day1.Interfaces;
using Day1.Models;

namespace Day1.Repositories;

public class PartRepository : IRepository<Part>
{
    public void Add(Part entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"INSERT INTO Parts (PartName, Price)
                       VALUES (@PartName, @Price);";

        connection.Execute(sql, entity);
    }

    public Part? GetById(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM Parts WHERE PartId = @id;";
        return connection.QuerySingleOrDefault<Part>(sql, new { id });
    }

    public IEnumerable<Part> GetAll()
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM Parts;";
        return connection.Query<Part>(sql);
    }

    public void Update(Part entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"UPDATE Parts
                       SET PartName = @PartName,
                           Price = @Price
                       WHERE PartId = @PartId;";

        connection.Execute(sql, entity);
    }

    public void Delete(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "DELETE FROM Parts WHERE PartId = @id;";
        connection.Execute(sql, new { id });
    }
}