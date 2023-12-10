using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class PersonFactory : ICrossingFactory//фабрика для человека
{
    public ICrossingObject CreateVehicle()
    {
        return new Person();
    }
}