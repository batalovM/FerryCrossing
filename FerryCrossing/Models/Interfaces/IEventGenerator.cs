namespace FerryCrossing.Models;

public interface IEventGenerator
{
    double GenerateNormalEvent();
    double GenerateExponentialEvent();
}