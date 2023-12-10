using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class TruckFactory: ICrossingFactory//фабрика для грузовой машины
{
    public ICrossingObject CreateVehicle()
    {
        return new Truck();
    }
}