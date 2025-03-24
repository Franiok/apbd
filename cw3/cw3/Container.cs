namespace cw3;

public abstract class Container
{
    private static int _counter = 1;
    public double ContainerWeight { get;  }
    public double Height { get; }
    public double LoadWeight { get; set; }
    public double Depth { get; }
    public string SerialNum { get; }
    public double MaxLoad { get; }

    public Container(string type, double containerWeight, double height, double depth, double maxLoad)
    {
        SerialNum = $"KON-{type}-{_counter++}";
        ContainerWeight = containerWeight;
        Height = height;
        Depth = depth;
        MaxLoad = maxLoad;
    }

    public virtual void Load(double weight)
    {
        if (LoadWeight + weight > MaxLoad)
            throw new Exception("OverfillException: load won't fit");
        LoadWeight += weight;
    }
    
    public virtual void Unload()
    {
        LoadWeight = 0;
    }

    public override string ToString()
    {
        return $"{SerialNum}: {LoadWeight}/{MaxLoad}kg, Depth: {Depth}cm, " +
               $"Height: {Height}cm, ContainerWeight: {ContainerWeight}kg";
    }
}