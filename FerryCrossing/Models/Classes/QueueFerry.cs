using System;
using System.Collections.Generic;
using System.Linq;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class QueueFerry
{
    private Ferry f1;
    private Ferry f2;
    public QueueFerry(Ferry f1, Ferry f2)
    {
        this.f1 = f1;
        this.f2 = f2;
    }

    private readonly List<ICrossingFactory> factories = new()
    {
        new PersonFactory(),
        new TruckFactory(),
        new CarFactory(), 
    };
    // private int _localPerson;
    // private int _localCar;
    // private int _localTruck;
    private readonly Queue<ICrossingObject> _queue = new(100);
    
    private void AddToQueue(Queue<ICrossingObject> queue)
    {
        var random = new Random();
        var randomNum = random.Next(0, factories.Count);
        var factory = factories[randomNum];
        var vehicle = factory.CreateVehicle();
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
        var _localPerson = 0;
        var _localCar = 0;
        var _localTruck = 0;
        var i1 = 0;
        var i2 = 0;
        while (_queue.Count != 70)
        {
            AddToQueue(_queue);   
        }
        Console.WriteLine("start");
        while (totalSumFirst <= timeWork / 2)
        {
            if(_localPerson>=f2.CapacityPerson + f1.CapacityPerson && _localCar>=f2.CapacityCar + f1.CapacityCar && _localTruck>=f2.CapacityTruck + f1.CapacityTruck) break;
            var obj = _queue.Peek();
            switch (obj.Type)
            {
                case "Person":
                    _localPerson++;
                    break;
                case "Car":
                    _localCar++;
                    break;
                case "Truck":
                    _localTruck++;
                    break;
            }
            switch (obj.Type)
            {
                case "Person" when _localPerson >= f2.CapacityPerson + f1.CapacityPerson:
                    _queue.Dequeue();
                    break;
                case "Car" when _localCar >= f2.CapacityCar + f1.CapacityCar:
                    _queue.Dequeue();
                    break;
                case "Truck" when _localTruck >= f2.CapacityTruck + f1.CapacityTruck:
                    _queue.Dequeue();
                    break;
            }
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
            //AddToQueue(_queue);
            if (!(totalSumFirst > timeWork / 2)) continue;
            totalSumFirst -= _dataFirst.Last();
            _dataFirst.Remove(_dataFirst.Last());
            break;
        }

        while (totalSumSecond <= timeWork / 2)
        {
            if(_localPerson>=f2.CapacityPerson + f1.CapacityPerson && _localCar>=f2.CapacityCar + f1.CapacityCar && _localTruck>=f2.CapacityTruck + f1.CapacityTruck) break;
            var obj = _queue.Peek();
            switch (obj.Type)
            {
                case "Person":
                    _localPerson++;
                    break;
                case "Car":
                    _localCar++;
                    break;
                case "Truck":
                    _localTruck++;
                    break;
            }
            switch (obj.Type)
            {
                case "Person" when _localPerson >= f2.CapacityPerson + f1.CapacityPerson:
                    _queue.Dequeue();
                    break;
                case "Car" when _localCar >= f2.CapacityCar + f1.CapacityCar:
                    _queue.Dequeue();
                    break;
                case "Truck" when _localTruck >= f2.CapacityTruck + f1.CapacityTruck:
                    _queue.Dequeue();
                    break;
            }
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
            //AddToQueue(_queue);
             if (!(totalSumSecond > timeWork / 2)) continue;
             totalSumSecond -= _dataFirst.Last();
             _dataFirst.Remove(_dataFirst.Last());
             break;
        
        }
          
        var totalSum = totalSumFirst + totalSumSecond;
        Console.WriteLine(totalSum);
        Console.WriteLine($"Количество людей: {_localPerson}");
        Console.WriteLine($"Количество машин: {_localCar}");
        Console.WriteLine($"Количество грузовых машин: {_localTruck}");
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