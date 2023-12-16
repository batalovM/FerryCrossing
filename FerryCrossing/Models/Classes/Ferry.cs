using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class Ferry
{
    private readonly List<ICrossingFactory> factories = new()
    {
        new TruckFactory(),
        new PersonFactory(), 
        new CarFactory(), 
    };
    
    private Queue<ICrossingObject> data1 = new(10);
    private void AddToQueue(Queue<ICrossingObject> queue)
    {
        var random = new Random();
        var randomNum = random.Next(0, factories.Count);
        var factory = factories[randomNum];
        var vehicle = factory.CreateVehicle();
        queue.Enqueue(vehicle);
    }

    private readonly List<double> _data = new(100);
     public List<double> ProcessQueueWithMultipleThreads(double start, double end)
    {
        var lock1 = new object();
        var lock2 = new object();
        var timeWork = end - start;
        double totalSum = 0;
        var count = 0;
        var countPersons = 0;
        var countTruck = 0;
        var countCar = 0;
        var evg = new EventGenerator();
        while (data1.Count != 10)
        {
            AddToQueue(data1);   
        }
        var task1 = Task.Run(() =>
        {
            lock (lock1)
            {
                 Console.WriteLine("Start first thread");
                 while (totalSum < timeWork)
                 {
                     if (data1.Peek().Type is "Car")
                     {
                         data1.Dequeue();
                         var add = 7 + evg.GenerateNormalEvent();
                         totalSum += add;
                         _data.Add(add);
                         countCar += 1;
                         Console.WriteLine("Обработана Машина из 1 потока");
                     }
                     else if (data1.Peek().Type is "Truck")
                     {
                         data1.Dequeue();
                         var add = 20 + evg.GenerateNormalEvent();
                         totalSum += add;
                         _data.Add(add);
                         countTruck += 1;
                         Console.WriteLine("Обработан Грузовой из 1 потока");
                     }
                     else if (data1.Peek().Type is "Person")
                     {
                         var add = evg.GenerateNormalEvent();
                         totalSum += add;
                         _data.Add(add);
                         countPersons += 1;
                         Console.WriteLine("Обработан человек из 1 потока");
                     }
                     count++;
                     Console.WriteLine();
                 }
            }
        });
        var task2 = Task.Run(() =>
        {
            lock (lock2)
            {
                Console.WriteLine("Start second thread");
                while (totalSum < timeWork)
                {
                    if (data1.Peek().Type is "Car")
                    {
                        data1.Dequeue();
                        var add = 7 + evg.GenerateExponentialEvent();
                        totalSum += add;
                        _data.Add(add);
                        countCar += 1;
                        Console.WriteLine("Обработана Машина из 2 потока");
                    }
                    else if (data1.Peek().Type is "Truck")
                    {
                        var add = 20 + evg.GenerateExponentialEvent();
                        totalSum += add;
                        _data.Add(add);
                        countTruck += 1;
                        Console.WriteLine("Обработан грузовой из 2 потока");
                    }
                    else if (data1.Peek().Type is "Person")
                    {
                        var add = evg.GenerateExponentialEvent();
                        totalSum += add;
                        _data.Add(add);
                        countPersons += 1;
                        Console.WriteLine("Обработана Человек из 2 потока");
                    }
                    count++;
                }
            }
        });
        Task.WaitAll(task1, task2);
        Console.WriteLine($"Количество обработанных объектов:{count}");
        Console.WriteLine($"Количество объектов person :{countPersons}");
        Console.WriteLine($"Количество объектов car:{countCar}");
        Console.WriteLine($"Количество объектов truck :{countTruck}");
        return _data;
        
    }
    
}