namespace cw3;

class Program
{
    public static void Main(string[] args)
    {
        Ship ship = new Ship("Statek1", 100, 5, 100);
        GasContainer gasContainer1 = new GasContainer(10, 10, 10, 10, 15);
        LiquidContainer liquidContainer1 = new LiquidContainer(true, 10, 10, 10, 15);
        RefrigeratedContainer refrigeratedContainer1 = new RefrigeratedContainer("Bananas", 15, 10, 10, 10, 15);
        Console.WriteLine(ship);
        // Overfill Exception działa: gasContainer1.Load(100);
        gasContainer1.Load(14);
        liquidContainer1.Load(10);
        liquidContainer1.Load(5);
        refrigeratedContainer1.Load(15);
        ship.LoadContainer(gasContainer1);
        ship.LoadContainer(liquidContainer1);
        ship.LoadContainer(refrigeratedContainer1);
        Console.WriteLine(gasContainer1);
        Console.WriteLine(liquidContainer1);
        Console.WriteLine(refrigeratedContainer1);
        Console.WriteLine(ship);
        ship.UnloadContainer(refrigeratedContainer1);
        Console.WriteLine(ship);
    }
}