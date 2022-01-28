using InventoryLibrary;
using System;
using System.Collections.Generic;

namespace Product_Inventory
{
    public class Program
    {
        public enum Menu
        {
            Add,//добавить запись
            Delete,//удалить запись
            PrintConsol,//вывести на консоль
            Save,//записать в файл
            Close,//закрыть программу
            Load,//считаь данные из файла
            Null,
            Change,//изменить данные
            Summ//сумма выбранных товаров
        }

        static string Path = $"{Environment.CurrentDirectory}\\InventDataList.json";

        static void Main(string[] args)
        {
            List<Product> SheetProduct = new();
            FileIOService FileIoService = new(Path);
            Inventori inventori = new();
            Menu menu = Menu.Load;
            Console.Title = "Ведомость по инвентаризации";
            string msgError = "Ошибка ввода, попробуйте еще раз:";
            MenuShow();
            do
            {
                switch (menu)
                {
                    case Menu.Load:
                        {
                            Console.WriteLine("Загрузка данных из файла.....");
                            SheetProduct = FileIoService.LoadData();
                            if (SheetProduct.Count == 0)
                            {
                                Console.WriteLine("В списке товаров нет записей !!!\nДобавте новую запись в список:");

                                menu = Menu.Add;

                                continue;
                            }
                            else
                            {
                                inventori.PrintSheet(SheetProduct);
                            }
                            break;

                        }
                    case Menu.Add:
                        {
                            //Console.Clear();
                            Console.WriteLine("Добавить новую запись в таблицу....");
                            Console.WriteLine("Введите наименование товара, не более 15 символов:");
                            string NameProduct = Console.ReadLine();
                            
                            NameProduct = IsTextNullLen(NameProduct, msgError);
                           
                            Console.WriteLine("Введите количество едениц товара:");
                            string NumUnit = Console.ReadLine();
                            
                            double NumberUnits = CheckEnterNumber(NumUnit, msgError, 0.001);
                           
                            Console.WriteLine("Введите стоимость еденицы товара:");
                            string PriceUnit = Console.ReadLine();
                           
                            decimal UnitPrice = (decimal)CheckEnterNumber(PriceUnit, msgError, 0.01);

                           
                            int IdProduct = IDGenerator.GetID(SheetProduct);
                            SheetProduct.Add(new Product(NameProduct, UnitPrice, NumberUnits,IdProduct));
                            inventori.PrintSheet(SheetProduct);
                            
                            break;
                        }
                    case Menu.Delete:
                        {
                            Console.WriteLine("Удалить запись из таблицы списком...");
                            Console.WriteLine("введите ID товаров через \"-\" например 2-4-7 или 4:");
                            string Summ = Console.ReadLine();
                           
                            Summ = IsTextNullLen(Summ, msgError);
                            inventori.DeleteProduсtList(SheetProduct,Summ); 
                            break;
                        }
                    case Menu.Save:
                        {
                            Console.WriteLine("Сохранить текущие изменения в файл....");
                            FileIoService.SaveData(SheetProduct);
                            SheetProduct = FileIoService.LoadData();
                            inventori.PrintSheet(SheetProduct);
                            break;
                        }
                    case Menu.PrintConsol:
                        {
                            Console.WriteLine("Печать текущей таблицы....");
                            inventori.PrintSheet(SheetProduct);
                            break;
                        }
                    case Menu.Summ:
                        {
                            Console.WriteLine("Сложение стоимости товаров...");
                            Console.WriteLine("введите ID товаров через \"+\" например 2+4+7:");
                            string Summ = Console.ReadLine();
                            
                            Summ = IsTextNullLen(Summ,msgError);
                            inventori.SummProduktList(SheetProduct, Summ);
                            break;
                        }
                    case Menu.Null:
                        {
                            Console.WriteLine("Выберите действия из МЕНЮ:");
                            break;
                        }
                    case Menu.Change:
                        {
                            Console.WriteLine(menu.ToString());
                            break;
                        }

                }
                menu = GetMenu();
            } while (menu != Menu.Close);
        }


        //Вывод на консоль клавиш управления програмой
        static void MenuShow()
        {
            Console.WriteLine("--МЕНЮ" + new String('-', 33));
            Console.WriteLine("| Добавить новую запись: ALT+A        |\n" +
                              "| Удалить запись: ALT+D               |\n" +
                              "| Стоимость выбранных товаров: ALT+T  |\n" +
                              "| Показать список товаров: ALT+P      |\n" +
                              "| Сохранить изменения: ALT+S          |\n" +
                              "| Закрыть программу: CTRL+С           |\n" +
                              "| Измениь продукт: ALT+Q              |\n" +
                              "| Загрузить файл: ALT+L               |");
            Console.Write(new String('-', 39));
            Console.WriteLine();
        }

        //Проверка наименования
        static string IsTextNullLen(string Text, string Messag)
        {
            Text = Text.Trim(' ');
            while (string.IsNullOrEmpty(Text))
            {
                Console.WriteLine(Messag);
                Text = Console.ReadLine().Trim(' ');
            }
            Text = Text.Trim().ToLower();
            if (Text.Length > 15)
            {
                Text = Text.Substring(0, 15);
            }
            //написание с большой буквы
            char tmp = char.ToUpper(Text[0]);
            Text = Text.Substring(1, Text.Length - 1);
            return $"{tmp}{Text}";

        }

        //метод для проверки вводимых значений с консоли на число и по условию > minValue
        static double CheckEnterNumber(string enterText, string message, double minValue)
        {
            bool flag;
            double number;
            do
            {
                flag = double.TryParse(enterText, out number);
                if (!flag || number < minValue)
                {
                    Console.WriteLine(message);
                    flag = false;
                    enterText = Console.ReadLine();
                }

            } while (!flag);
            return Math.Round(number, 3);
        }

        // перехват клавиш управления
        static Menu GetMenu()
        {
            // Не допускать выхода из программы при нажатии CTRL+C.
            //Console.TreatControlCAsInput = true;

            Menu menu = Menu.Null;
            ConsoleKeyInfo keyEnter = Console.ReadKey(true); 
            switch (keyEnter.Key)
            {
                case ConsoleKey.D:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Alt) != 0)
                        {
                            menu = Menu.Delete;
                        };
                        break;
                    }
                case ConsoleKey.A:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Alt) != 0)
                        {
                            menu = Menu.Add;
                        }
                        break;
                    }
                case ConsoleKey.P:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Alt) != 0)
                        {
                            menu = Menu.PrintConsol;
                        }
                        break;
                    }
                case ConsoleKey.Q:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Alt) != 0)
                        {
                            menu = Menu.Change;
                        }
                        break;
                    }
                case ConsoleKey.S:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Alt) != 0)
                        {
                            menu = Menu.Save;
                        }
                        break;
                    }
                case ConsoleKey.L:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Alt) != 0)
                        {
                            menu = Menu.Load;
                        }
                        break;
                    }
                case ConsoleKey.T:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Alt) != 0)
                        {
                            menu = Menu.Summ;
                        }
                        break;
                    }
               

            }
            return menu;
        }


    }
}
