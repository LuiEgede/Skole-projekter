using Day1.Interfaces;
using Day1.Models;

namespace Day1.Services;

public class CustomerService
{
    private readonly IRepository<Customer> _repository;

    public CustomerService(IRepository<Customer> repository)
    {
        _repository = repository;
    }

    public void CreateCustomer(string customerName, string phone, string? email, string user)
    {
        var customer = new Customer
        {
            CustomerName = customerName,
            Phone = phone,
            Email = email
        };

        _repository.Add(customer);

        LogService.Log(
            user: user,
            operation: "CREATE",
            tableName: "Customers",
            recordId: customer.CustomerId,
            oldData: null,
            newData: $"Name={customer.CustomerName}, Phone={customer.Phone}, Email={customer.Email}");
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _repository.GetAll();
    }
}