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
            List<Product> sheetProduct=new();
            FileIOService fileIoService = new(Path);
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
                            sheetProduct = fileIoService.LoadData();
                            if (sheetProduct.Count == 0)
                            {
                                Console.WriteLine("В списке товаров нет записей !!!\nДобавте новую запись в список:");

                                menu = Menu.Add;

                                continue;
                            }
                            else
                            {
                                inventori.PrintSheet(sheetProduct);
                            }
                            break;

                        }
                    case Menu.Add:
                        {
                            Console.WriteLine("Добавить новую запись в таблицу....");
                            Console.WriteLine("Введите наименование товара, не более 15 символов:");
                            string nameProduct = Console.ReadLine();
                            
                            nameProduct = IsTextNullLen(nameProduct, msgError);
                           
                            Console.WriteLine("Введите количество едениц товара:");
                            string numUnit = Console.ReadLine();
                            
                            double numberUnits = CheckEnterNumber(numUnit, msgError, 0.001);
                           
                            Console.WriteLine("Введите стоимость еденицы товара:");
                            string priceUnit = Console.ReadLine();
                           
                            decimal unitPrice = (decimal)CheckEnterNumber(priceUnit, msgError, 0.01);

                           
                            int IdProduct = IDGenerator.GetID(sheetProduct);
                            sheetProduct.Add(new Product(nameProduct, unitPrice, numberUnits,IdProduct));
                            inventori.PrintSheet(sheetProduct);
                            
                            break;
                        }
                    case Menu.Delete:
                        {
                            Console.WriteLine("Удалить запись из таблицы списком...");
                            Console.WriteLine("введите ID товаров через \"-\" например 2-4-7 или 4:");
                            string summ = Console.ReadLine();
                           
                            summ = IsTextNullLen(summ, msgError);
                            inventori.DeleteProduсtList(sheetProduct,summ); 
                            break;
                        }
                    case Menu.Save:
                        {
                            Console.WriteLine("Сохранить текущие изменения в файл....");
                            fileIoService.SaveData(sheetProduct);
                            sheetProduct = fileIoService.LoadData();
                            inventori.PrintSheet(sheetProduct);
                            break;
                        }
                    case Menu.PrintConsol:
                        {
                            Console.WriteLine("Печать текущей таблицы....");
                            inventori.PrintSheet(sheetProduct);
                            break;
                        }
                    case Menu.Summ:
                        {
                            Console.WriteLine("Сложение стоимости товаров...");
                            Console.WriteLine("введите ID товаров через \"+\" например 2+4+7:");
                            string summ = Console.ReadLine();
                            
                            summ = IsTextNullLen(summ,msgError);
                            inventori.SummProduktList(sheetProduct, summ);
                            break;
                        }
                    case Menu.Null:
                        {
                            Console.WriteLine("Выберите действия из МЕНЮ:");
                            break;
                        }
                    case Menu.Change:
                        {
                            Console.WriteLine("Изменить запись в таблице ...");
                            Console.WriteLine("Введите ID товара.... ");
                            string strChange = Console.ReadLine();

                            int changeID = CheckEnterNumber(strChange, msgError, 1);
                            // if (SheetProduct.Exists(x => x.ProductID == changeID))
                            int index = sheetProduct.FindIndex(x => x.productID == changeID);
                            if (index!=-1)
                            {

                               //int index = SheetProduct.FindIndex(x => x.ProductID == changeID);

                                Console.WriteLine("Введите наименование товара, не более 15 символов:");
                                string nameProduct = Console.ReadLine();

                                if (!string.IsNullOrEmpty(nameProduct.Trim()))
                                {
                                    nameProduct = IsTextNullLen(nameProduct, msgError);
                                    sheetProduct[index].nameProduct = nameProduct;
                                }

                                Console.WriteLine("Введите количество едениц товара:");
                                string numUnit = Console.ReadLine();

                                if (!string.IsNullOrEmpty(numUnit.Trim()))
                                {
                                    double NumberUnits = CheckEnterNumber(numUnit, msgError, 0.001);
                                    sheetProduct[index].numberUnits = NumberUnits;
                                }

                                Console.WriteLine("Введите стоимость еденицы товара:");
                                string priceUnit = Console.ReadLine();

                                if (!string.IsNullOrEmpty(priceUnit.Trim()))
                                {
                                    decimal UnitPrice = (decimal)CheckEnterNumber(priceUnit, msgError, 0.01);
                                    sheetProduct[index].unitPrice = UnitPrice;
                                }
                                inventori.PrintSheet(sheetProduct);

                            }
                            else
                            { 
                               Console.WriteLine($"Товара с ID={changeID} в списке нет....\n" +
                                                 $"Продолжить ALT+Q.");
                            }

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
            Console.WriteLine("| Добавить новую запись: ALT+A             |\n" +
                              "| Удалить запись: ALT+D                    |\n" +
                              "| Стоимость выбранных товаров: ALT+T       |\n" +
                              "| Показать список товаров: ALT+P           |\n" +
                              "| Сохранить изменения: ALT+S               |\n" +
                              "| Закрыть программу без сохранения: CTRL+С |\n" +
                              "| Измениь продукт: ALT+Q                   |\n" +
                              "| Загрузить список из файла: ALT+L         |");
            Console.Write(new String('-', 39));
            Console.WriteLine();
        }

        //Проверка наименования
        static string IsTextNullLen(string text, string message)
        {
            text = text.Trim(' ');
            while (string.IsNullOrEmpty(text))
            {
                Console.WriteLine(message);
                text = Console.ReadLine().Trim(' ');
            }
            text = text.Trim().ToLower();
            if (text.Length > 15)
            {
                text = text.Substring(0, 15);
            }
            //написание с большой буквы
            char tmp = char.ToUpper(text[0]);
            text = text.Substring(1, text.Length - 1);
            return $"{tmp}{text}";

        }

        //методы для проверки вводимых значений с консоли на число и по условию > minValue
        static double CheckEnterNumber(string enterText, string message, double minValue)
        {

            return CheckEnterNumber<double>(enterText.Replace('.',','), message, (x) =>
            {
                var isParced = double.TryParse(x, out var resulValue);
                if (isParced)
                {
                    resulValue = Math.Round(resulValue, 3);
                }
                return (isParced, resulValue);

            },
           (x) => x < minValue);

        }

        static int CheckEnterNumber(string enterText, string message, int minValue)
        {
            return CheckEnterNumber<int>(enterText, message, (x) =>
            {
                var isParced = int.TryParse(x, out var resulValue);
                return (isParced, resulValue);

            },
            (x) => x < minValue);

        }


        static T CheckEnterNumber<T>(string enterText,
            string message,
            Func<string,(bool,T)>tryparse,
            Func<T,bool> check)
        {
            bool flag;
            T number;
            do
            {
                (flag,number)=tryparse(enterText);
               
                if (!flag || check(number))
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
