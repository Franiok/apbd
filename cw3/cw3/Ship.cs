namespace cw3;

using System.Collections.Generic;

public class Ship
{
    public string Name { get; }
    public double MaxSpeed { get; }
    public int MaxContainers { get; }
    public double MaxWeight { get; }
    private List<Container> _containers = new();

    public Ship(string name, double maxSpeed, int maxContainers, double maxWeight)
    {
        Name = name;
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxWeight = maxWeight;
    }

    public void LoadContainer(Container container)
    {
        if (_containers.Count >= MaxContainers ||
            TotalWeight() + container.MaxLoad + container.ContainerWeight > MaxWeight)
            Console.WriteLine("Ship capacity exceeded.");
        else
        {
            _containers.Add(container);
        }
    }

    public void UnloadContainer(Container container)
    {
        _containers.Remove(container);
    }

    public double TotalWeight() => _containers.Sum(c => c.MaxLoad + c.ContainerWeight);

    public override string ToString()
    {
        return $"Ship {Name}: {_containers.Count} containers loaded.";
    }
}