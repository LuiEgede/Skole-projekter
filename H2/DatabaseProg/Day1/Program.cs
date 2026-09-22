using Day1.Data;
using Day1.Services;
using Microsoft.EntityFrameworkCore;

using var context = new AppDbContext();

context.Database.Migrate();
DbSeeder.Seed(context);
TriggerSetup.CreateWorkOrderStatusTrigger();

Console.WriteLine("Cross fingers and hope it all works out!");


// Test to see if trigger works by updating workOrderStatus
var workOrder = context.WorkOrders.FirstOrDefault();

if (workOrder != null)
{
    workOrder.WorkStatus = "Cancelled";
    context.SaveChanges();
}

var service = new WorkOrderService();

// Test CREATE
service.CreateWorkOrder(
    carId: 1,
    workDescription: "Test logging order",
    workStatus: "Open",
    user: "peter");

// Test UPDATE
service.UpdateWorkOrderStatus(
    workOrderId: 1,
    newStatus: "Completed",
    user: "peter");

// Test DELETE
service.DeleteWorkOrder(
    workOrderId: 2,
    user: "peter");


