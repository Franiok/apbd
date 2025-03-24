namespace cw3;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; }
    public double Temperature { get; }

    private static readonly Dictionary<string, double> ProductTemperatures = new()
    {
        {"Bananas", 13.3},
        {"Chocolate", 18},
        {"Fish", 2},
        {"Meat", -15},
        {"Ice Cream", -18},
        {"Frozen pizza", -30},
        {"Cheese", 7.2},
        {"Sausages", 5},
        {"Butter", 20.5},
        {"Eggs", 19}
    };

    public RefrigeratedContainer(string productType, double temperature, double containerWeight, 
        double height, double depth, double maxLoad)
        : base("C", containerWeight, height, depth, maxLoad)
    {
        if (!ProductTemperatures.ContainsKey(productType))
        {
            throw new Exception($"Product not available: {productType}");
        }

        if (temperature < ProductTemperatures[productType])
        {
            throw new Exception($"Temperature too low. Temperature required for {productType}: {temperature}");
        }
        ProductType = productType;
        Temperature = temperature;
    }
    
    public override string ToString()
    {
        return base.ToString() + $", ProductType: {ProductType}, Temperature: {Temperature}";
    }
}