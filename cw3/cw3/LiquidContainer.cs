namespace cw3;

public class LiquidContainer: Container, IHazardNotifier
{
    public bool IsHazardous { get; }

    public LiquidContainer(bool isHazardous, double containerWeight, double height, double depth, double maxLoad)
        : base("L", containerWeight, height, depth, maxLoad)
    {
        IsHazardous = isHazardous;
    }

    public override void Load(double weight)
    {
        double limit = IsHazardous ? MaxLoad * 0.5 : MaxLoad * 0.9;
        if (LoadWeight + weight > limit)
        {
            HazardNotification("Limit overflow");
        }
        else
        {
            base.Load(weight);
        }
    }

    public void HazardNotification(string message)
    {
        Console.WriteLine($"WARNING: {SerialNum} - {message}");
    }
}