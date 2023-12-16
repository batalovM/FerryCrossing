using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
    
    private Queue<ICrossingObject> _queue = new(10);
    private void AddToQueue(Queue<ICrossingObject> queue)
    {
        var random = new Random();
        var randomNum = random.Next(0, factories.Count);
        var factory = factories[randomNum];
        var vehicle = factory.CreateVehicle();
        queue.Enqueue(vehicle);
    }

    private readonly List<double> _dataFirst = new(100);
    private readonly List<double> _dataSecond = new(100);
    public List<double> ProcessQueueWithMultipleThreads(double start, double end, Ferry ferry1, Ferry ferry2)
    {
        var lock1 = ferry1;
        var lock2 = ferry2;
        var timeWork = end - start;
        double totalSum = 0;
        var count = 0;
        var countPersons = 0;
        var countTruck = 0;
        var countCar = 0;
        var evg = new EventGenerator();
        while (_queue.Count != 10)
        {
            AddToQueue(_queue);   
        }
        var task1 = Task.Run(() =>
        {
            lock (lock1)
            {
                 Console.WriteLine("Start first thread");
                 while (totalSum < timeWork)
                 {
                     if (_queue.Peek().Type is "Car")
                     {
                         _queue.Dequeue();
                         var add = 7 + evg.GenerateNormalEvent();
                         totalSum += add;
                         _dataFirst.Add(add);
                         countCar += 1;
                         Console.WriteLine("Обработана Машина из 1 потока");
                     }
                     else if (_queue.Peek().Type is "Truck")
                     {
                         _queue.Dequeue();
                         var add = 20 + evg.GenerateNormalEvent();
                         totalSum += add;
                         _dataFirst.Add(add);
                         countTruck += 1;
                         Console.WriteLine("Обработан Грузовой из 1 потока");
                     }
                     else if (_queue.Peek().Type is "Person")
                     {
                         var add = evg.GenerateNormalEvent();
                         totalSum += add;
                         _dataFirst.Add(add);
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
                    if (_queue.Peek().Type is "Car")
                    {
                        _queue.Dequeue();
                        var add = 7 + evg.GenerateExponentialEvent();
                        totalSum += add;
                        _dataSecond.Add(add);
                        countCar += 1;
                        Console.WriteLine("Обработана Машина из 2 потока");
                    }
                    else if (_queue.Peek().Type is "Truck")
                    {
                        var add = 20 + evg.GenerateExponentialEvent();
                        totalSum += add;
                        _dataSecond.Add(add);
                        countTruck += 1;
                        Console.WriteLine("Обработан грузовой из 2 потока");
                    }
                    else if (_queue.Peek().Type is "Person")
                    {
                        var add = evg.GenerateExponentialEvent();
                        totalSum += add;
                        _dataSecond.Add(add);
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
        Console.WriteLine($"Размер списка из первого потока {_dataFirst.Count}");
        Console.WriteLine($"Размер списка из второго потока {_dataSecond.Count}");
        var returnList = _dataFirst.ToList();
        returnList.AddRange(_dataSecond);
        return returnList;
        }
    
}