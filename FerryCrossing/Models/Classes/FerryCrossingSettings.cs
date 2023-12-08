using System;

namespace FerryCrossing.Models;

public abstract class FerryCrossingSettings
{
    public string Route { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public bool EnableCargoLoading { get; set; }
    public int WaitTime { get; set; }
    public bool StaffGoesForLunch { get; set; }
    public bool NonPassengerCars { get; set; }
    public string WeatherConditions { get; set; }   
    
}