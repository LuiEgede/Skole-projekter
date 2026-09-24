using Dapper;
using Day1.Data;
using Day1.Interfaces;
using Day1.Models;

namespace Day1.Repositories;

public class CustomerRepository : IRepository<Customer>
{
    public void Add(Customer entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"INSERT INTO Customers (CustomerName, Phone, Email)
                       VALUES (@CustomerName, @Phone, @Email);";

        connection.Execute(sql, entity);
    }

    public Customer? GetById(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM Customers WHERE CustomerId = @id;";
        return connection.QuerySingleOrDefault<Customer>(sql, new { id });
    }

    public IEnumerable<Customer> GetAll()
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "SELECT * FROM Customers;";
        return connection.Query<Customer>(sql);
    }

    public void Update(Customer entity)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = @"UPDATE Customers
                       SET CustomerName = @CustomerName,
                           Phone = @Phone,
                           Email = @Email
                       WHERE CustomerId = @CustomerId;";

        connection.Execute(sql, entity);
    }

    public void Delete(int id)
    {
        using var connection = MySqlConnectionFactory.CreateConnection();
        string sql = "DELETE FROM Customers WHERE CustomerId = @id;";
        connection.Execute(sql, new { id });
    }
}