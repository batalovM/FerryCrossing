
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using FerryCrossing.Models.Classes;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Newtonsoft.Json;
using ReactiveUI;
using SkiaSharp;

namespace FerryCrossing.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private const string Path = @"C:\Users\batal\RiderProjects\FerryCrossing\FerryCrossing\Assets\database.json";
    private double _startTime;
    public double StartTime {get => _startTime; set => this.RaiseAndSetIfChanged(ref _startTime, value); }
    private double _endTime;
    public double EndTime {get => _endTime; set => this.RaiseAndSetIfChanged(ref _endTime, value); }
    private bool _enableCargoLoading;
    public bool EnableCargoLoading { get => _enableCargoLoading; set => this.RaiseAndSetIfChanged(ref _enableCargoLoading, value); }
    private bool _staffGoesForLunch;
    public bool StaffGoesForLunch { get => _staffGoesForLunch; set => this.RaiseAndSetIfChanged(ref _staffGoesForLunch, value); }
    private bool _nonPassengerCars;
    public bool NonPassengerCars { get => _nonPassengerCars; set => this.RaiseAndSetIfChanged(ref _nonPassengerCars, value); }
    private int _weatherConditions;
    public int WeatherConditions { get => _weatherConditions; set => this.RaiseAndSetIfChanged(ref _weatherConditions, value); }

    private string _textBox1 = "";

    public string TextBox1 { get => _textBox1; set => this.RaiseAndSetIfChanged(ref _textBox1, value); }
    public List<string> WeatherList { get; set; } = new() { "Солнечно", "Дождливо", "Снежно", "Шторм" };
    public ISeries[] Series { get; set; } =
    {
        new ColumnSeries<double>
        {
            Values = Data, 
            Fill = new SolidColorPaint(SKColors.Blue) 
        }
    };
    public LabelVisual Title { get; set; } = new()
    {
        Text = "Результаты моделирования",
        TextSize = 20,
        Padding = new LiveChartsCore.Drawing.Padding(15),
    };
    public ReactiveCommand<Unit, Unit> GenerateChart { get; }
    private static List<double> Data { get; set; } = new();
    private QueueFerry QueueFerryList { get; set; } = null!;

    public MainWindowViewModel()
    {
        LoadJson();
        GenerateChart = ReactiveCommand.Create(UpdateChart);
    }
    private void UpdateChart()
    {
        Data.Clear(); 
        var start = StartTime;
        var end = EndTime;
        var f1 = new Ferry();
        var f2 = new Ferry(); 
        QueueFerryList = new QueueFerry(f1, f2);
        var data = QueueFerryList.ProcessQueue(start, end, EnableCargoLoading, StaffGoesForLunch, NonPassengerCars);
        Data.AddRange(data);
        Series = new ISeries[] { new ColumnSeries<double> {Values = Data, Fill = new SolidColorPaint(SKColors.Blue)} };
        this.RaisePropertyChanged(nameof(Data));
        this.RaisePropertyChanged(nameof(Series));
        if (WeatherConditions == 0) TextBox1 = "Прибытие через 1 час";
        if (WeatherConditions == 1) TextBox1 = "Прибытие через 2 часа";
        if (WeatherConditions == 2) TextBox1 = "Прибытие через 3 часа";
        if (WeatherConditions == 3) TextBox1 = "Рейс откладывается \nдо улучшения \nпогодных условий";
        SaveData();
    }
    private void SaveData()
    {
        var d = new DataBase
        {
            _startTime = StartTime,
            _endTime = EndTime,
            _enableCargoLoading = EnableCargoLoading,
            _staffGoesForLunch = StaffGoesForLunch,
            _nonPassengerCars = NonPassengerCars,
            _list = Data,
            _text = TextBox1
        };
        Console.WriteLine(d);
        var str = JsonConvert.SerializeObject(d, Formatting.Indented);
        File.WriteAllText(Path,str);
    }

    private void LoadJson()
    {
        var str = File.ReadAllText(Path);
        var data = JsonConvert.DeserializeObject<DataBase>(str);
        StartTime = data!._startTime;
        EndTime = data._endTime;
        EnableCargoLoading = data._enableCargoLoading;
        StaffGoesForLunch = data._staffGoesForLunch;
        NonPassengerCars = data._nonPassengerCars;
        Data = data._list;
        TextBox1 = data._text;
        Series = new ISeries[] { new ColumnSeries<double> {Values = Data, Fill = new SolidColorPaint(SKColors.Blue)} };
        this.RaisePropertyChanged(nameof(Series));
    }
}

