
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FerryCrossing.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using ReactiveUI;
using SkiaSharp;
using static System.Drawing.Brushes;

namespace FerryCrossing.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private string _startTime;
    public string StartTime
    {
        get => _startTime;
        set => this.RaiseAndSetIfChanged(ref _startTime, value);
    }
    private string _endTime;
    public string EndTime
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
    
    public ISeries[] Series { get; set; } = { new ColumnSeries<double> { Values = Data, Fill = new SolidColorPaint(SKColors.Blue) } };

    private static void addT()
    {
        var generate = new EventGenerator();
        for (var i = 1; i < 10; i++)
        {
            var value = generate.GenerateNormalEvent() + 10; // Пример генерации данных с нормальным распределением
            Data.Add(value);
        }
    }

    public LabelVisual Title { get; set; } = new()
    {
        Text = "Результаты моделирования",
        TextSize = 20,
        Padding = new LiveChartsCore.Drawing.Padding(15),
    };

    public ICommand GenerateChart { get; }
    private static List<double> Data = new();
    public MainWindowViewModel()
    {
        addT();
        GenerateChart = ReactiveCommand.CreateFromTask(UpdateChart);
        
    }

    private static Task UpdateChart()
    {
        Data.Clear(); // Очистить старые данные
        // addT(); // Добавить новые данные
        // Series = new ISeries[] { new ColumnSeries<double> { Values = Data, Fill = new SolidColorPaint(SKColors.Blue) } }; // Обновить график
        return Task.CompletedTask;
    }
}

