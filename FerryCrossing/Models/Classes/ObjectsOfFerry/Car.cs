using FerryCrossing.Models.Interfaces;

namespace FerryCrossing.Models.Classes;

public class Car :  ICrossingObject
{
   public string Type => "Car";
   public double Delay => 7;
}