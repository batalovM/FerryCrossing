using System.Collections.Generic;

namespace FerryCrossing.Models;

// Класс для визуализации результатов моделирования с использованием графиков
public class GraphResultsVisualizer : IResultsVisualizer
{
    private IGraphPlotter graphPlotter;

    public GraphResultsVisualizer(IGraphPlotter graphPlotter)
    {
        this.graphPlotter = graphPlotter;
    }

    public void VisualizeResults()
    {
        // Логика получения результатов моделирования и передачи их на визуализацию
        // ...
        List<IStatistics> statistics = new List<IStatistics>(); // Пример данных для визуализации
        graphPlotter.PlotGraph(statistics);
    }
}