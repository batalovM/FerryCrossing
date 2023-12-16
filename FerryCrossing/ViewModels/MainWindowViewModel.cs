﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using FerryCrossing.Models.Classes;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using ReactiveUI;
using SkiaSharp;

namespace FerryCrossing.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private double _startTime;
    public double StartTime
    {
        get => _startTime;
        set => this.RaiseAndSetIfChanged(ref _startTime, value);
    }
    private double _endTime;
    public double EndTime
    {
        get => _endTime;
        set => this.RaiseAndSetIfChanged(ref _endTime, value);
    }
    private bool _enableCargoLoading;
    public bool EnableCargoLoading
    {
        get => _enableCargoLoading;
        set => this.RaiseAndSetIfChanged(ref _enableCargoLoading, value);
    }
    private bool _staffGoesForLunch;
    public bool StaffGoesForLunch
    {
        get => _staffGoesForLunch;
        set => this.RaiseAndSetIfChanged(ref _staffGoesForLunch, value);
    }

    private bool _nonPassengerCars;
    public bool NonPassengerCars
    {
        get => _nonPassengerCars;
        set => this.RaiseAndSetIfChanged(ref _nonPassengerCars, value);
    }

    private string _weatherConditions;
    public string WeatherConditions
    {
        get => _weatherConditions;
        set => this.RaiseAndSetIfChanged(ref _weatherConditions, value);
    }
    //переменные для связки с xaml
    
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
    private void addT(double start, double end, Ferry ferry)
    {
        FerryList = new Ferry();
        var data = FerryList.ProcessQueueWithMultipleThreads(start, end);

        Console.WriteLine($"Количество данных в списке: {data.Count}");
        foreach (var d in data)
        {
            Data.Add(d);
        }
    }

    public ReactiveCommand<Unit, Unit> GenerateChart { get; }
    private static List<double> Data { get;} = new();
    private Ferry FerryList { get; set; }

    public MainWindowViewModel()
    {
        GenerateChart = ReactiveCommand.Create(UpdateChart);
    }
    private void UpdateChart()
    {
        Data.Clear(); // Очистить старые данные
        var start = StartTime;
        var end = EndTime;
        FerryList = new Ferry();
        var data = FerryList.ProcessQueueWithMultipleThreads(start, end);
        Console.WriteLine($"Количество данных в списке: {data.Count}");
        foreach (var d in data)
        {
            Data.Add(d);
        }
        //addT(start, end, FerryList);
        Console.WriteLine(Data.Count);
        // var data = FerryList.ProcessQueueWithMultipleThreads(start, end);
        // Console.WriteLine($"Количество данных в списке: {data.Count}");
        // foreach (var d in data)
        // {
        //     Data.Add(d);
        // }
        //
         Series = new ISeries[] { new ColumnSeries<double> {Values = Data, Fill = new SolidColorPaint(SKColors.Blue)} };
        this.RaisePropertyChanged(nameof(Data));
        this.RaisePropertyChanged(nameof(Series));
    }
}

