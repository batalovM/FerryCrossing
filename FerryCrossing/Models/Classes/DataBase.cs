using System.Collections.Generic;
using LiveChartsCore;
using Newtonsoft.Json;

namespace FerryCrossing.Models.Classes;

public class DataBase
{
    public double _startTime { get; set; }
    public double _endTime { get; set; }
    public bool _enableCargoLoading{ get; set; }
    public bool _staffGoesForLunch{ get; set; }
    public bool _nonPassengerCars{ get; set; }
    public List<double> _list{ get; set; }
    public string _text { get; set; }
    //public ISeries[] _series{ get; set; }

    // public DataBase(double startTime, double endTime, bool enableCargoLoading, bool staffGoesForLunch, bool nonPassengerCars, List<double> list, ISeries[] series)
    // {
    //     _startTime = startTime;
    //     _endTime = endTime;
    //     _enableCargoLoading = enableCargoLoading;
    //     _staffGoesForLunch = staffGoesForLunch;
    //     _nonPassengerCars = nonPassengerCars;
    //     _list = list;
    //     _series = series;
    // }

    public override string ToString()
    {
        return $"StartTime: {_startTime} EndTime: {_endTime} EnableCargoLoading: {_enableCargoLoading} StaffGoesForLunch: {_staffGoesForLunch} NonPassegerCars: {_nonPassengerCars} List: {_list}";
    }
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}