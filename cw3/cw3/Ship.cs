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

    public void DisplayCargo()
    {
        foreach (var c in _containers)
        {
            Console.WriteLine(c);
        }
    }
    
    public void ShipSwap(Ship ship, Container container)
    {
        if (ship._containers.Count >= ship.MaxContainers ||
            ship.TotalWeight() + container.MaxLoad + container.ContainerWeight > ship.MaxWeight)
        {
            Console.WriteLine("Can't swap, target ship capacity exceeded.");
        }
        else
        {
            UnloadContainer(container);
            ship.LoadContainer(container);
        }
    }
    
    public void SwapContainers(string serialNum, Container containerToLoad)
    {
        var containerToRemove = _containers.Find(c => c.SerialNum == serialNum);
        if (containerToRemove != null)
        {
            if (_containers.Count >= MaxContainers ||
                TotalWeight() + containerToLoad.MaxLoad + containerToLoad.ContainerWeight > MaxWeight)
                Console.WriteLine("Can't swap, ship capacity exceeded.");
            else
            {
                UnloadContainer(containerToRemove);
                LoadContainer(containerToLoad);
            }
        }
    }
    
    public void LoadContainers(List<Container> containerList)
    {
        foreach (var c in containerList)
        {
            LoadContainer(c);
        }
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
        return $"Ship {Name}: ContainerAmount: {_containers.Count}/{MaxContainers}, CargoMaxWeight: {TotalWeight()}/{MaxWeight}kg, MaxSpeed: {MaxSpeed}";
    }
}