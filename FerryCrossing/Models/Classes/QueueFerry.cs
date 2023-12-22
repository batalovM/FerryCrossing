using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class QueueFerry
{
    private readonly Ferry _f1;
    private readonly Ferry _f2;
    public QueueFerry(Ferry f1, Ferry f2)
    {
        _f1 = f1;
        _f2 = f2;
    }

    private readonly List<ICrossingFactory> factories = new()
    {
        new TruckFactory(),
        new PersonFactory(), 
        new CarFactory(), 
    };
    private int _localPerson;
    private int _localCar;
    private int _localTruck;
    //private readonly Queue<ICrossingObject> _queue = new(100);
    
    private void AddToQueue(Queue<ICrossingObject> queue)
    {
        var random = new Random();
        var randomNum = random.Next(0, factories.Count);
        var factory = factories[randomNum];
        var vehicle = factory.CreateVehicle();
       queue.Enqueue(vehicle);
    }

    private readonly List<double> _dataFirst = new(200);

    public List<double> ProcessQueue(double start, double end)
    {
        Queue<ICrossingObject> _queue = new(100);
        var timeWork = end - start;
        double totalSumFirst = 0;
        double totalSumSecond = 0;
        var evg = new EventGenerator();
        var localPerson = 0;
        var localCar = 0;
        var localTruck = 0;
        while (_queue.Count != 100)
        {
            AddToQueue(_queue);   
        }
        
        Console.WriteLine("start");
        while (totalSumFirst <= timeWork / 2)
        {
            var obj = _queue.Peek();
            _queue.Dequeue();
            var add = obj.Delay + evg.GenerateNormalEvent();
            totalSumFirst += (add);
            _dataFirst.Add(add);
            AddToQueue(_queue);
            if (!(totalSumFirst > timeWork / 2)) continue;
            totalSumFirst -= _dataFirst.Last();
            _dataFirst.Remove(_dataFirst.Last());
            break;
        }

        while (totalSumSecond <= timeWork / 2)
        {
            var obj = _queue.Peek();
            _queue.Dequeue();
            var add = obj.Delay + evg.GenerateExponentialEvent();
            totalSumSecond += (add);
            _dataFirst.Add(add);

            AddToQueue(_queue);
             if (!(totalSumSecond > timeWork / 2)) continue;
             totalSumSecond -= _dataFirst.Last();
             _dataFirst.Remove(_dataFirst.Last());
             break;

        }
        

        var totalSum = totalSumFirst + totalSumSecond;
        Console.WriteLine(totalSum);
        Console.WriteLine($"Количество людей:{localPerson}");
        Console.WriteLine($"Количество машин: {localCar}");
        Console.WriteLine($"Количество грузовых:{localTruck}");
        return _dataFirst;
    }
}