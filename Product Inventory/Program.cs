using System;
using System.Collections.Generic;
using InventoryLibrary;

namespace Product_Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ведомость по инвентаризации";
            MenuShow();

            List<Product> SheetProd = new ();

            SheetProd.Add (new Product ("яблоки",45.5m,20,SheetProd));
            SheetProd.Add (new Product ("вишня",20m,15,SheetProd));
            SheetProd.Add(new Product("бананы", 30m, 10, SheetProd));
            
            PrintSheet (SheetProd);
            
            
            Console.Read();
        }

        static void MenuShow()
        {   Console.WriteLine(new String('-',31));
            Console.WriteLine("| Добавить новую запись Insert" + "|\n" +
                "| Удалить запись Delete"+"       |\n" +
                "| Показать список товаров ??? " + "|\n" +
                "| Сохранить изменения ???     " + "|\n" +
                "| Закрыть программу ???       "+"|");
            Console.Write(new String('-',31));
            Console.WriteLine();
        }
        static void PrintSheet(List<Product> product)
        {   Console.WriteLine(new String('-', 47));
            Console.WriteLine("| {0,-5} | {1,-15} | {2,-6} | {3,-8} |","Инв","Наименование","Кол-во","Цена");
            Console.WriteLine(new String('-', 47));

            foreach (Product p in product)
            {   
                Console.WriteLine(p.ToString());
                Console.WriteLine(new String('-', 47));
            }
        }


    }
}
