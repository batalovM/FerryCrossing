using System;
using System.Collections.Generic;

namespace FerryCrossing.Models;
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
        const double lambda = 0.5;
        var u = Random.NextDouble(); // рандомное число от 0 до 1
        return -Math.Log(1 - u) / lambda; // генерация экспоненциального распределения
    }
}