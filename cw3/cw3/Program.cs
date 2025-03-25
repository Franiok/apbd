namespace cw3;

class Program
{
    public static void Main(string[] args)
    {
        Ship ship1 = new Ship("Statek1", 100, 5, 100);
        GasContainer gasContainer1 = new GasContainer(10, 10, 100, 100, 15);
        LiquidContainer liquidContainer1 = new LiquidContainer(true, 10, 100, 100, 15);
        RefrigeratedContainer refrigeratedContainer1 = new RefrigeratedContainer("Bananas", 15, 10, 100, 100, 15);
        Console.WriteLine(ship1);
        // Overfill Exception działa: gasContainer1.Load(100);
        gasContainer1.Load(14);
        liquidContainer1.Load(10);
        liquidContainer1.Load(5);
        refrigeratedContainer1.Load(15);
        ship1.LoadContainer(gasContainer1);
        ship1.LoadContainer(liquidContainer1);
        ship1.LoadContainer(refrigeratedContainer1);
        Console.WriteLine(gasContainer1);
        Console.WriteLine(liquidContainer1);
        Console.WriteLine(refrigeratedContainer1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        ship1.UnloadContainer(refrigeratedContainer1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        ship1.UnloadContainer(gasContainer1);
        ship1.UnloadContainer(liquidContainer1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        List<Container> list1 = new List<Container>();
        list1.Add(gasContainer1);
        list1.Add(liquidContainer1);
        list1.Add(refrigeratedContainer1);
        ship1.LoadContainers(list1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        ship1.UnloadContainer(refrigeratedContainer1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        ship1.SwapContainers("KON-G-1", refrigeratedContainer1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        Ship ship2 = new Ship("Statek2", 100, 10, 30);
        Console.WriteLine(ship2);
        ship2.DisplayCargo();
        ship1.ShipSwap(ship2, refrigeratedContainer1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        Console.WriteLine(ship2);
        ship2.DisplayCargo();
        ship1.ShipSwap(ship2, liquidContainer1);
        Console.WriteLine(ship1);
        ship1.DisplayCargo();
        Console.WriteLine(ship2);
        ship2.DisplayCargo();
    }
}