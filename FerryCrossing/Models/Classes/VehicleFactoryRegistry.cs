using System;
using System.Collections.Generic;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class VehicleFactoryRegistry // класс для рандомной генерации объектов в очередь
{
    private readonly Dictionary<string, ICrossingFactory> _factories = new();
    private Queue<ICrossingObject> _queue = new();
    private static VehicleFactoryRegistry factoryRegistry = new();

    public void RegisterFactory(string vehicleType, ICrossingFactory factory)
    {
        _factories.Add(vehicleType, factory);

    }
    
    public ICrossingFactory GetFactory(string vehicleType)
    {
        return (_factories.TryGetValue(vehicleType, value: out var factory) ? factory : null)!;
    }

    public void ManageVehicles(DateTime startTime, DateTime endTime)
    {
        while (DateTime.Now < endTime)
        {
            if (_queue.Count < 10)
            {
                AddToQueue();
            }

            if (_queue.Count > 0)
            {
                var vehicle = _queue.Dequeue();
            }
        }
    }

    private void AddToQueue()
    {
        var random = new Random();
        var vehicleTypes = new[] { "Person", "Car", "Truck" };
        var randomVehicleType = vehicleTypes[random.Next(0, vehicleTypes.Length)];
        var factory = factoryRegistry.GetFactory(randomVehicleType);
        if (factory != null)
        {
            var vehicle = factory.CreateVehicle();
            _queue.Enqueue(vehicle);
        }
    }
}


