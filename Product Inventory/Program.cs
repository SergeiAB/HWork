using System;
using InventoryLibrary;

namespace Product_Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ведомость по инвентаризации";
            Product product = new Product();
            product.PrintConsol();
            Console.Read();
        }
    }
}
