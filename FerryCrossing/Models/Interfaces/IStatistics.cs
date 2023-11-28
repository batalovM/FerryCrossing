namespace FerryCrossing.Models;

// Интерфейс для сбора статистических данных
public interface IStatistics
{
    int NumberOfCars { get; set; }
    int NumberOfPeople { get; set; }
}