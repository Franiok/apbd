namespace cw3;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; }
    public double Temperature { get; }

    public RefrigeratedContainer(string productType, double temperature, double containerWeight, 
        double height, double depth, double maxLoad)
        : base("C", containerWeight, height, depth, maxLoad)
    {
        ProductType = productType;
        Temperature = temperature;
        // Temperatura dla oddzielnych produktów
    }
    
    // ToString()
}