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
            Esc,//отменить зменения
            Null
        }

        static string Path = $"{Environment.CurrentDirectory}\\InventDataList.json";

        static void Main(string[] args)
        {
            List<Product> SheetProduct = new();
            FileIOService FileIoService = new(Path);
            Inventori inventori = new Inventori();
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

                            SheetProduct.Add(new Product(NameProduct, UnitPrice, NumberUnits, SheetProduct));
                            inventori.PrintSheet(SheetProduct);
                            break;
                        }
                    case Menu.Delete:
                        {
                            Console.WriteLine("Удалить запись из таблицы...");
                            Console.WriteLine("Введите инвентарный №\nдля удаления товара:");
                            int DeleteProdID = CheckEnterNumber(Console.ReadLine(), msgError, 1);

                            inventori.DeleteProduct(SheetProduct, DeleteProdID);
               
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
            } while (menu != Menu.Close);
        }


        //Вывод на консоль клавиш управления програмой
        static void MenuShow()
        {
            Console.WriteLine("--МЕНЮ" + new String('-', 28));
            Console.WriteLine("| Добавить новую запись: CTRL+A   " + "|\n" +
                "| Удалить запись: CTRL+D          " + "|\n" +
                "| Показать список товаров: CTRL+P " + "|\n" +
                "| Сохранить изменения: CTRL+S     " + "|\n" +
                "| Закрыть программу: CTRL+Q       " + "|\n" +
                "| Загрузить файл: CTRL+L          " + "|");
            Console.Write(new String('-', 34));
            Console.WriteLine();
        }

        //Проверка наименования
        static string IsTextNullLen(string Text, string Messag)
        {
            Text = Text.Trim(' ', '\t');
            while (String.IsNullOrEmpty(Text))
            {
                Console.WriteLine(Messag);
                Text = Console.ReadLine().Trim(' ', '\t');
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

        static int CheckEnterNumber(string enterText, string message, int minValue)
        {
            bool flag;
            int number;
            do
            {
                flag = int.TryParse(enterText, out number);
                if (!flag || number < minValue)
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
                case ConsoleKey.D:
                    {
                        if ((keyEnter.Modifiers & ConsoleModifiers.Control) != 0)
                        {
                            menu = Menu.Delete;
                        };
                        break;
                    }
                case ConsoleKey.A:
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
