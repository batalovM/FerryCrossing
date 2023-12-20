

using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class CarFactory : ICrossingFactory//фабрика для машины
{
    public string Type { get; set; } = "Car";

    public ICrossingObject CreateVehicle()
    {
        return new Car();
    }
}