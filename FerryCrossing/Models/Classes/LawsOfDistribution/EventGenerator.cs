using System;
using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;
//класс реализации законов распределения 
public class EventGenerator : IEventGenerator
{
    private static readonly Random Random = new();
    public double GenerateNormalEvent()
    {
        var r = new NormalRandom();
        return r.NextDouble();
    }
    
    public double GenerateExponentialEvent()
    {
        const double lambda = 0.1;
        var u = Random.NextDouble(); // рандомное число от 0 до 1
        return (-Math.Log(1 - u) / lambda) + 2; // генерация экспоненциального распределения
    }
}