namespace FerryCrossing.Models;

// Класс для конкретной модели паромной переправы
public class SpecificFerryModeling : FerryModeling
{
    public SpecificFerryModeling(FerryCrossing ferryCrossing) : base(ferryCrossing) { }

    public override void SimulateFerryCrossing()
    {
        // Логика моделирования паромной переправы
        // ...
    }
}