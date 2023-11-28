namespace FerryCrossing.Models;
// Базовый класс для моделирования паромной переправы
public abstract class FerryModeling : IFerryModeling
{
    protected FerryCrossing ferryCrossing;

    public FerryModeling(FerryCrossing ferryCrossing)
    {
        this.ferryCrossing = ferryCrossing;
    }

    public abstract void SimulateFerryCrossing();
}
