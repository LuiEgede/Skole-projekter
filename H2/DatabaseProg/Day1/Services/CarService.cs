using Day1.Interfaces;
using Day1.Models;

namespace Day1.Services;

public class CarService
{
    private readonly IRepository<Car> _repository;

    public CarService(IRepository<Car> repository)
    {
        _repository = repository;
    }

    public void CreateCar(int customerId, string licensePlate, string brand, string model, int manufactureYear,
        string user)
    {
        var car = new Car
        {
            CustomerId = customerId,
            LicensePlate = licensePlate,
            Brand = brand,
            Model = model,
            ManufactureYear = manufactureYear
        };

        _repository.Add(car);

        LogService.Log(
            user: user,
            operation: "CREATE",
            tableName: "Cars",
            recordId: car.CarId,
            oldData: null,
            newData:
            $"CustomerId={car.CustomerId}, LicensePlate={car.LicensePlate}, Brand={car.Brand}, Model={car.Model}, Year={car.ManufactureYear}");
    }

    public IEnumerable<Car> GetAllCars()
    {
        return _repository.GetAll();
    }
}