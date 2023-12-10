namespace FerryCrossing.Models.Interfaces;

public interface IEventGenerator
{
    double GenerateNormalEvent();
    double GenerateExponentialEvent();
}