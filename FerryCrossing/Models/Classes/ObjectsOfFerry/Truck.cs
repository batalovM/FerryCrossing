using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class Truck : Object, ICrossingObject
{
    
    public string Type => "Truck";
    public double Delay => 20;
}