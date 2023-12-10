using System.Collections.Generic;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class VehicleFactoryRegistry// класс для рандомной генерации объектов в очередь
{
    private readonly Dictionary<string, ICrossingFactory> _factories = new();

    public void RegisterFactory(string vehicleType, ICrossingFactory factory)
    {
        _factories.Add(vehicleType, factory);
                
    }
    public ICrossingFactory GetFactory(string vehicleType)
    {
        return (_factories.TryGetValue(vehicleType, value: out var factory) ? factory : null)!;
    }
}