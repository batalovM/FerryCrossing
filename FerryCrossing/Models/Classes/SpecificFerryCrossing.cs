using System.Collections.Generic;

namespace FerryCrossing.Models;

// Класс для паромной переправы на определенном маршруте
public class SpecificFerryCrossing : FerryCrossing
{
    public override void CollectStatistics(List<IStatistics> statistics)
    {
        // Реализация сбора статистических данных для конкретной паромной переправы
    }
}