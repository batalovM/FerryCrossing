using System.Collections.Generic;

namespace FerryCrossing.Models;

// Интерфейс для создания графиков
public interface IGraphPlotter
{
    void PlotGraph(List<IStatistics> statistics);
}