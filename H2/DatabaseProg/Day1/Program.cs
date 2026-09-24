using Day1.Repositories;
using Day1.Services;

// READ ME BEFORE START! 
// create customer first, then create car, and last you can create the work order.

var customerRepository = new CustomerRepository();
var carRepository = new CarRepository();
var workOrderRepository = new WorkOrderRepository();

var customerService = new CustomerService(customerRepository);
var carService = new CarService(carRepository);
var workOrderService = new WorkOrderService(workOrderRepository);

string user;
bool isAdmin;

Console.WriteLine("Enter user name:");
user = Console.ReadLine() ?? "unknown";

Console.WriteLine("Are you admin? (yes/no)");
string roleInput = Console.ReadLine()?.Trim().ToLower() ?? "no";
isAdmin = roleInput == "yes";

bool running = true;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1 - Create Customer");
    Console.WriteLine("2 - Show Customers");
    Console.WriteLine("3 - Create Car");
    Console.WriteLine("4 - Show Cars");
    Console.WriteLine("5 - Create WorkOrder");
    Console.WriteLine("6 - Show WorkOrders");
    Console.WriteLine("7 - Update WorkOrder status");
    Console.WriteLine("8 - Delete WorkOrder");
    Console.WriteLine("9 - Exit");
    Console.WriteLine("10 - Update WorkOrder status via stored procedure");

    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.WriteLine("Customer name:");
            string customerName = Console.ReadLine() ?? "";

            Console.WriteLine("Phone:");
            string phone = Console.ReadLine() ?? "";

            Console.WriteLine("Email:");
            string? email = Console.ReadLine();

            customerService.CreateCustomer(customerName, phone, email, user);
            Console.WriteLine("Customer created.");
            break;

        case "2":
            var customers = customerService.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine(
                    $"CustomerId: {customer.CustomerId}, Name: {customer.CustomerName}, Phone: {customer.Phone}, Email: {customer.Email}");
            }

            break;

        case "3":
            Console.WriteLine("CustomerId:");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid CustomerId.");
                break;
            }

            Console.WriteLine("License Plate:");
            string licensePlate = Console.ReadLine() ?? "";

            Console.WriteLine("Brand:");
            string brand = Console.ReadLine() ?? "";

            Console.WriteLine("Model:");
            string model = Console.ReadLine() ?? "";

            Console.WriteLine("Manufacture Year:");
            if (!int.TryParse(Console.ReadLine(), out int manufactureYear))
            {
                Console.WriteLine("Invalid Manufacture Year.");
                break;
            }

            carService.CreateCar(customerId, licensePlate, brand, model, manufactureYear, user);
            Console.WriteLine("Car created.");
            break;

        case "4":
            var cars = carService.GetAllCars();
            foreach (var car in cars)
            {
                Console.WriteLine(
                    $"CarId: {car.CarId}, CustomerId: {car.CustomerId}, Plate: {car.LicensePlate}, Brand: {car.Brand}, Model: {car.Model}, Year: {car.ManufactureYear}");
            }

            break;

        case "5":
            Console.WriteLine("CarId:");
            if (!int.TryParse(Console.ReadLine(), out int carId))
            {
                Console.WriteLine("Invalid CarId.");
                break;
            }

            Console.WriteLine("Work description:");
            string description = Console.ReadLine() ?? "";

            Console.WriteLine("Work status:");
            string status = Console.ReadLine() ?? "";

            workOrderService.CreateWorkOrder(carId, description, status, user);
            Console.WriteLine("WorkOrder created.");
            break;

        case "6":
            var workOrders = workOrderService.GetAllWorkOrders();
            foreach (var workOrder in workOrders)
            {
                Console.WriteLine(
                    $"WorkOrderId: {workOrder.WorkOrderId}, CarId: {workOrder.CarId}, Status: {workOrder.WorkStatus}, Description: {workOrder.WorkDescription}");
            }

            break;

        case "7":
            Console.WriteLine("WorkOrderId:");
            if (!int.TryParse(Console.ReadLine(), out int updateId))
            {
                Console.WriteLine("Invalid WorkOrderId.");
                break;
            }

            Console.WriteLine("New status:");
            string newStatus = Console.ReadLine() ?? "";

            workOrderService.UpdateWorkOrderStatus(updateId, newStatus, user);
            Console.WriteLine("WorkOrder updated.");
            break;

        case "8":
            Console.WriteLine("WorkOrderId:");
            if (!int.TryParse(Console.ReadLine(), out int deleteId))
            {
                Console.WriteLine("Invalid WorkOrderId.");
                break;
            }

            workOrderService.DeleteWorkOrder(deleteId, user, isAdmin);
            Console.WriteLine(isAdmin ? "Delete attempted." : "Delete denied. Admin only.");
            break;

        case "9":
            running = false;
            break;
        
        case "10":
            Console.WriteLine("WorkOrderId:");
            if (!int.TryParse(Console.ReadLine(), out int spId))
            {
                Console.WriteLine("Invalid WorkOrderId.");
                break;
            }

            Console.WriteLine("New status:");
            string spStatus = Console.ReadLine() ?? "";

            try
            {
                workOrderRepository.UpdateWorkOrderStatusWithProcedure(spId, spStatus);
                Console.WriteLine("WorkOrder updated via stored procedure.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            break;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}