using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class QueueFerry
{
    private readonly List<ICrossingFactory> factories = new()
    {
        new TruckFactory(),
        new PersonFactory(), 
        new CarFactory(), 
    };
    
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
    private readonly List<double> _dataSecond = new(200);

    public List<double> ProcessQueueWithMultipleThreads(double start, double end, Ferry f1, Ferry f2)
    {
        var lock1 = f1;
        var lock2 = f2;
        var timeWork = end - start;
        double totalSum = 0;
        var evg = new EventGenerator();
        while (_queue.Count != 10)
        {
            AddToQueue(_queue);
        }

        Console.WriteLine(_queue.Peek().Type);
            var task1 = Task.Run(() =>
            {
                lock (lock1)
                {
                    Console.WriteLine("Start first thread");
                    while (totalSum <= timeWork)
                    {
                        _queue.Dequeue();
                        var obj = _queue.Peek();
                        var add = obj.Delay + evg.GenerateNormalEvent();
                        totalSum += (add);
                        _dataFirst.Add(add);
                        AddToQueue(_queue);
                    }

                }
            });
            var task2 = Task.Run(() =>
            {
                lock (lock2)
                {
                    Console.WriteLine("Start second thread");
                while (totalSum <= timeWork)
                {
                    _queue.Dequeue();
                    var obj = _queue.Peek();
                    var add = obj.Delay + evg.GenerateExponentialEvent();
                    totalSum += (add);
                    _dataSecond.Add(add);
                    AddToQueue(_queue);
                }

                }
            });
            Task.WaitAll(task1, task2);
            Console.WriteLine(totalSum);
        var returnList = _dataFirst.ToList();
        returnList.AddRange(_dataSecond);
        return returnList;
    }
}