using Dapper;
using Day1.Data;
using Day1.Interfaces;
using Day1.Models;

namespace Day1.Repositories;

public class CarRepository : IRepository<Car>
{
    public void Add(Car entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"INSERT INTO Cars (CustomerId, LicensePlate, Brand, Model, ManufactureYear)
                       VALUES (@CustomerId, @LicensePlate, @Brand, @Model, @ManufactureYear);";

        connection.Execute(sql, entity);
    }

    public Car? GetById(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM Cars WHERE CarId = @id;";
        return connection.QuerySingleOrDefault<Car>(sql, new { id });
    }

    public IEnumerable<Car> GetAll()
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM Cars;";
        return connection.Query<Car>(sql);
    }

    public void Update(Car entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"UPDATE Cars
                       SET CustomerId = @CustomerId,
                           LicensePlate = @LicensePlate,
                           Brand = @Brand,
                           Model = @Model,
                           ManufactureYear = @ManufactureYear
                       WHERE CarId = @CarId;";

        connection.Execute(sql, entity);
    }

    public void Delete(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "DELETE FROM Cars WHERE CarId = @id;";
        connection.Execute(sql, new { id });
    }
}