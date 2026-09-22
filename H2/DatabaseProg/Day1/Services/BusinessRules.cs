using Day1.Models;

namespace Day1.Services;

public static class BusinessRules
{
    // Rule: do not create a customer if the email already exists.
    public static bool CanCreateCustomer(List<Customer> existingCustomers, string email)
    {
        return !existingCustomers.Any(c =>
            string.Equals(c.Email, email, StringComparison.OrdinalIgnoreCase));
    }

    // Rule: a work order can only be read together with its related car and customer.
    public static bool CanReadWorkOrder(WorkOrder? workOrder)
    {
        return workOrder != null && workOrder.Car != null && workOrder.Car.Customer != null;
    }

    // Rule: only allow updates when the new status is one of the allowed values.
    public static bool CanUpdateWorkStatus(string newStatus)
    {
        string[] allowedStatuses = { "Open", "In Progress", "Completed", "Cancelled" };
        return allowedStatuses.Contains(newStatus.Trim());
    }

    // Rule: only allow deleting a customer if no cars are associated with the customer.
    public static bool CanDeleteCustomer(Customer? customer)
    {
        return customer != null && !customer.Cars.Any();
    }
}