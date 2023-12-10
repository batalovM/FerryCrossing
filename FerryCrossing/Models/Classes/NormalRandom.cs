using System;

namespace FerryCrossing.Models.Classes;
//класс для реализации закона нормального распределения 
internal class NormalRandom : Random
{
    private double _prevSample = double.NaN;
    protected override double Sample()
    {
        var result = _prevSample;
        if (!double.IsNaN(_prevSample))
        {
            _prevSample = double.NaN;
            return result;
        }
        double u, v, s;
        do
        {
            u = 2 * base.Sample() - 1;
            v = 2 * base.Sample() - 1;
            s = u * u + v * v;
        }
        while (u <= -1 || v <= -1 || s >= 1 || s == 0);
        var r = Math.Sqrt(-2 * Math.Log(s) / s);
        _prevSample = r * v;
        return  r * u;
    }
}