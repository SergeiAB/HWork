using System;
using System.Collections.Generic;
using InventoryLibrary;

namespace Product_Inventory
{   
    internal class Program
    {
        public enum Menu
        {
            Add,//добавить запись
            Delete,//удалить запись
            PrintConsol,//вывести на консоль
            Save,//записать в файл
            Close,//закрыть программу
            Load,//считаь данные из файла
            Esc,//отменить зменения
            Null
        }
        
        static string Path = $"{Environment.CurrentDirectory}\\InventDataList.json";
        
        static void Main(string[] args)
        {
            List<Product> SheetProduct;
            FileIOService FileIO = new (Path);
            Inventori inventori=new();
            Menu menu = Menu.Load;
            Console.Title = "Ведомость по инвентаризации";
            MenuShow();
            do
            {
                switch (menu)
                {
                    case Menu.Load:
                        {
                            Console.WriteLine(menu.ToString());
                            SheetProduct = FileIO.LoadData();
                            //inventori.PrintSheet(SheetProduct);
                            break;
                        }
                    case Menu.Add:
                        {
                            Console.WriteLine(menu.ToString());
                            break;
                        }
                    case Menu.Delete:
                        {
                            Console.WriteLine(menu.ToString());
                            break;
                        }
                    case Menu.Save:
                        {
                            Console.WriteLine(menu.ToString());
                            break;
                        }
                    case Menu.PrintConsol:
                        {
                            Console.WriteLine(menu.ToString());
                            break;
                        }
                    case Menu.Close:
                        {
                            Console.WriteLine(menu.ToString());
                            break;
                        }
                    case Menu.Null:
                        {
                            Console.WriteLine("Выберите действия из МЕНЮ:");
                            break;
                        }
                    case Menu.Esc:
                        {
                            Console.WriteLine(menu.ToString());
                            break;
                        }

                }



                menu = GetMenu();

               
            }while (menu!=Menu.Close);
 
        }


        //Вывод на консоль клавиш управления програмой
        static void MenuShow()
        {   Console.WriteLine("--МЕНЮ"+new String('-',28));
            Console.WriteLine("| Добавить новую запись: CTRL+N   "+"|\n" +
                "| Удалить запись: Delete          "+"|\n" +
                "| Показать список товаров: CTRL+P "+"|\n" +
                "| Сохранить изменения: CTRL+S     "+"|\n" +
                "| Закрыть программу: CTRL+Q       "+"|\n" +
                "| Загрузить файл: CTRL+L          "+"|");
            Console.Write(new String('-',34));
            Console.WriteLine();
        }


        //метод для проверки вводимых значений с консоли на число и по условию >= minValue & <= maxValue
        static int CheckEnterNumber(string enterText, string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            bool flag;
            int number;
            do
            {
                flag = int.TryParse(enterText, out number);
                if (!flag || number < minValue || number > maxValue)
                {
                    Console.WriteLine(message);
                    flag = false;
                    enterText = Console.ReadLine();
                }
            } while (!flag);
            return number;
        }

        // перехват клавиш управления
        static Menu GetMenu()
        {
            // Не допускать выхода из программы при нажатии CTRL+C.
            Console.TreatControlCAsInput = true;

            Menu menu = Menu.Null;
            ConsoleKeyInfo keyEnter = Console.ReadKey(true);
            switch (keyEnter.Key)
            {
                case ConsoleKey.Delete:
                    {
                        menu = Menu.Delete;
                        break;
                    }
                case ConsoleKey.N:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Control) != 0)
                        {
                            menu = Menu.Add;
                        }
                        break;
                    }
                case ConsoleKey.P:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Control) != 0)
                        {
                            menu = Menu.PrintConsol;
                        }
                        break;
                    }
                case ConsoleKey.Q:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Control) != 0)
                        {
                            menu = Menu.Close;
                        }
                        break;
                    }
                case ConsoleKey.S:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Control) != 0)
                        {
                            menu = Menu.Save;
                        }
                        break;
                    }
                case ConsoleKey.L:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Control) != 0)
                        {
                            menu = Menu.Load;
                        }
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        menu = Menu.Esc;
                        break;
                    }

            }
            return menu;
        }


    }
}
