using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class QueueFerry
{
    public QueueFerry(Ferry f1, Ferry f2)
    {
    }

    private readonly List<ICrossingFactory> factories = new()
    {
        new PersonFactory(),
        new TruckFactory(),
        new CarFactory(), 
    };
    private int _localPerson;
    private int _localCar;
    private int _localTruck;
    private readonly Queue<ICrossingObject> _queue = new(100);
    
    private void AddToQueue(Queue<ICrossingObject> queue)
    {
        var random = new Random();
        var randomNum = random.Next(0, factories.Count);
        var factory = factories[randomNum];
       var vehicle = factory.CreateVehicle();
       // var vehicle = factories[0].CreateVehicle();
       queue.Enqueue(vehicle);
    }

    private readonly List<double> _dataFirst = new(200);

    public List<double> ProcessQueue(double start, double end, bool enableCargoLoading, bool staffGoesForLunch, bool nonPassengerCars)
    {
        //Сделать просто максимальную вместительность 30
        var timeWork = end - start;
        double totalSumFirst = 0;
        double totalSumSecond = 0;
        var evg = new EventGenerator();
        var i1 = 0;
        var i2 = 0;
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
            if (enableCargoLoading)
            {
                add += EnableCargoLoading(obj);
            }

            if (nonPassengerCars)
            {
                add += NonPassengerCars(obj);
            }
            totalSumFirst += (add);
            if (staffGoesForLunch && totalSumFirst > StaffGoesForLunch(start, end))
            {
                while (i1<3)
                {
                  _dataFirst.Add(0);
                  i1++;
                }
                if(i1 > 3) break;
            }
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
            if (enableCargoLoading)
            {
                add += EnableCargoLoading(obj);
            }
            if (nonPassengerCars)
            {
                add += NonPassengerCars(obj);
            }
            totalSumSecond += (add);
            if (staffGoesForLunch && totalSumSecond > StaffGoesForLunch(start, end))
            {
                while (i2<3)
                {
                    _dataFirst.Add(0);
                    i2++;
                }
                if(i2 > 3) break;
            }
            _dataFirst.Add(add);
        
            AddToQueue(_queue);
             if (!(totalSumSecond > timeWork / 2)) continue;
             totalSumSecond -= _dataFirst.Last();
             _dataFirst.Remove(_dataFirst.Last());
             break;
        
        }
          
        var totalSum = totalSumFirst + totalSumSecond;
        Console.WriteLine(totalSum);
        return _dataFirst;
    }

    private int EnableCargoLoading(ICrossingObject obj)
    {
        const int plus = 10;
        return obj.Type == "Person" ? plus : 0;
    }

    private double StaffGoesForLunch(double start, double end)
    {
        var totalTime = end - start;
        return totalTime / 2;
    }

    private int NonPassengerCars(ICrossingObject obj)
    {
        const int plus = 13;
        return obj.Type == "Car" ? plus : 0;
    }
}