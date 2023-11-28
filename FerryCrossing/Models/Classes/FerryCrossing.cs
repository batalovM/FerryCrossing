using System.Collections.Generic;

namespace FerryCrossing.Models;
// Базовый класс для паромной переправы
public abstract class FerryCrossing
{
    public string Route { get; set; }
    public abstract void CollectStatistics(List<IStatistics> statistics);
}



