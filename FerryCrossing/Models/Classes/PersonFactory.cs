using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class PersonFactory : ICrossingFactory//фабрика для человека
{
    public string Type { get; set; } = "Person";

    public ICrossingObject CreateVehicle()
    {
        return new Person();
    }
}