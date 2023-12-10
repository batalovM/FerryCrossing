namespace FerryCrossing.Models.Interfaces;

public interface ICrossingFactory //Abstract Factory для возможности добавить новый объект 
{
    ICrossingObject CreateVehicle();
}