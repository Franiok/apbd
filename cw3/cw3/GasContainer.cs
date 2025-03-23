namespace cw3;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; }

    public GasContainer(double pressure, double containerWeight, double height, double depth, double maxLoad)
        : base("G", containerWeight, height, depth, maxLoad)
    {
        Pressure = pressure;
    }

    public override void Unload()
    {
        LoadWeight *= 0.05;
    }

    public void HazardNotification(string message)
    {
        Console.WriteLine($"WARNING: {SerialNum} - {message}");
    }

    public override string ToString()
    {
        return base.ToString() + $", Pressure: {Pressure}psi";
    }
}