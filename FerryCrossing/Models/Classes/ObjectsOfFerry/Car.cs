using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class Car : Object, ICrossingObject
{
   public string Type => "Car";
   public double Delay => 7;
}