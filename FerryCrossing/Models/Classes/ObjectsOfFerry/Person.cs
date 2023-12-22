using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class Person :  ICrossingObject
{
    public string Type => "Person";
    public double Delay => 0;
}