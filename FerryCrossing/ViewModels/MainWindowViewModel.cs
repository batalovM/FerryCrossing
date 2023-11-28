

using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace FerryCrossing.ViewModels;

public class MainWindowViewModel : ObservableObject
{

    public ISeries[] Series { get; set; } =
    {
        new LineSeries<double>
        {
            Values = new double[]{2, 1, 3, 4, 5, 6, 7},
            Fill = null
        }
    };  
    public LabelVisual Title { get; set; } = new()
    {
        Text = "Результаты моделирования",
        TextSize = 20,
        Padding = new LiveChartsCore.Drawing.Padding(15),
        Paint = new SolidColorPaint(SKColors.Blue)
    };
}