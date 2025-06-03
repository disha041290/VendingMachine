using System;
using VendingMachine; // Adjust namespace to match your actual class namespace

class Program
{
    static void Main()
    {
        IVendingMachineRepository vendingMachine = new VendingMachineRepository();

        Console.WriteLine("=== Welcome to the Vending Machine ===");

        while (true)
        {
            Console.WriteLine("\n1. Insert Coin");
            Console.WriteLine("2. Select Product");
            Console.WriteLine("3. Show Display");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Enter coin (nickel, dime, quarter, penny): ");
                    string coinInput = Console.ReadLine()?.ToLower();
                    Coin coin = coinInput switch
                    {
                        "nickel" => new Coin(5.0m, 21.21m),
                        "dime" => new Coin(2.268m, 17.91m),
                        "quarter" => new Coin(5.670m, 24.26m),
                        "penny" => new Coin(2.5m, 19.05m),
                        _ => new Coin(0, 0)
                    };
                    vendingMachine.InsertCoin(coin);
                    Console.WriteLine($"Display: {vendingMachine.GetDisplay()}");
                    break;

                case "2":
                    Console.WriteLine("Enter product (cola, chips, candy): ");
                    string product = Console.ReadLine();
                    var result = vendingMachine.SelectProduct(product);
                    if (result != null)
                    {
                        Console.WriteLine(result);
                    }
                    Console.WriteLine($"Display: {vendingMachine.GetDisplay()}");
                    break;

                case "3":
                    Console.WriteLine($"Display: {vendingMachine.GetDisplay()}");
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
