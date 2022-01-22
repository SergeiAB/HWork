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
            Inventori inv = new Inventori ();

            SheetProd.Add (new Product ("яблоки",45.5m,20,SheetProd));
            SheetProd.Add (new Product ("вишня",20m,15,SheetProd));
            SheetProd.Add(new Product("бананы", 30m, 10, SheetProd));

            PrintSheet(SheetProd);
           
            inv.DeleteProduct(SheetProd, 3);

            PrintSheet(SheetProd);

            Console.Read();
        }


        //Вывод на консоль клавиш управления програмой
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

        //Печать списка товаров в ведомости
        static void PrintSheet(List<Product> product)
        {   Console.WriteLine(new String('-', 70));
            Console.WriteLine("| {0,-5} | {1,-15} | {2,-10} | {3,-12} | {4,-12} |","Инв №","Наименование","Кол-во ед.","Цена за ед.","Всего:");
            Console.WriteLine(new String('-', 70));
            decimal total = 0;
            foreach (Product p in product)
            {   
                Console.WriteLine(p.ToString());
                Console.WriteLine(new String('-', 70));

                decimal tmp = p.UnitPrice * (decimal)p.NumberUnits;
                total += tmp;
            }
            Console.WriteLine("|{0,52} | {1,12} |","Итого:",total.ToString("c2"));
            Console.WriteLine(new String('-', 70));
        }


    }
}
