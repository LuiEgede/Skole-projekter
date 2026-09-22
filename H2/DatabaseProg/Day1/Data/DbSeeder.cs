using Day1.Models;

namespace Day1.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Customers.Any())
            return;

        var customer1 = new Customer
        {
            CustomerName = "Peter Jensen",
            Phone = "12345678",
            Email = "peter@example.com"
        };

        var customer2 = new Customer
        {
            CustomerName = "Maria Hansen",
            Phone = "87654321",
            Email = "maria@example.com"
        };

        var car1 = new Car
        {
            Customer = customer1,
            LicensePlate = "AB12345",
            Brand = "Toyota",
            Model = "Corolla",
            ManufactureYear = 2018
        };

        var car2 = new Car
        {
            Customer = customer2,
            LicensePlate = "CD67890",
            Brand = "Volkswagen",
            Model = "Golf",
            ManufactureYear = 2020
        };

        var part1 = new Part
        {
            PartName = "Oil Filter",
            Price = 149.95m
        };

        var part2 = new Part
        {
            PartName = "Brake Pads",
            Price = 499.50m
        };

        var workOrder1 = new WorkOrder
        {
            Car = car1,
            StartDate = DateTime.Today.AddDays(-2),
            EndDate = DateTime.Today.AddDays(-1),
            WorkDescription = "Oil change and inspection",
            WorkStatus = "Completed"
        };

        var workOrder2 = new WorkOrder
        {
            Car = car2,
            StartDate = DateTime.Today,
            EndDate = null,
            WorkDescription = "Brake inspection",
            WorkStatus = "In Progress"
        };

        var workOrderPart1 = new WorkOrderPart
        {
            WorkOrder = workOrder1,
            Part = part1,
            Quantity = 1
        };

        var workOrderPart2 = new WorkOrderPart
        {
            WorkOrder = workOrder2,
            Part = part2,
            Quantity = 1
        };

        context.AddRange(
            customer1, customer2,
            car1, car2,
            part1, part2,
            workOrder1, workOrder2,
            workOrderPart1, workOrderPart2
        );

        context.SaveChanges();
    }
}