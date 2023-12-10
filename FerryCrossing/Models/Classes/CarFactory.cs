

using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class CarFactory : ICrossingFactory//фабрика для машины
{
    public ICrossingObject CreateVehicle()
    {
        return new Car();
    }
}