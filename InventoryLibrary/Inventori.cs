using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public class Inventori
    {
        //расчет общей стоимости товаров в ведомости Итого:
        public decimal TotalPrice(List<Product> products)
        {
            decimal total = 0;
            if (products.Count > 0)
            {
                foreach (Product product in products)
                {
                    decimal tmp = product.AmountPrice();
                    total += tmp;
                }
            }
            return total;

        }

        //Печать списка товаров в ведомости
        public void PrintSheet(List<Product> product, bool total = true, bool title = true)
        {
            if (title)
            {
                Console.WriteLine(new String('-', 70));
                Console.WriteLine("| {0,-5} | {1,-15} | {2,-10} | {3,-12} | {4,-12} |", "Инв №", "Наименование", "Кол-во ед.", "Цена за ед.", "Всего:");
            }

            Console.WriteLine(new String('-', 70));
            foreach (Product prod in product)
            {
                Console.WriteLine(prod.ToString());
                Console.WriteLine(new String('-', 70));
            }

            if (total)
            {
                decimal inTotal = TotalPrice(product);
                Console.WriteLine("|{0,52} | {1,12} |", "Итого:", inTotal.ToString("c2"));
                Console.WriteLine(new String('-', 70));
            }

        }

        //сумирует товары
        public void SummProduktList(List<Product> product, string strKitsummId)
        {       
            string[] str = strKitsummId.Trim('+').Split('+');//закидываем в массив строку из символов
            int[] idProd = new int[str.Length];
            for (int i = 0; i < str.Length; i++)//в массив int копируем строчный массив
            {
                try
                {
                    idProd[i] = int.Parse(str[i]);
                }
                catch(Exception Ex) 
                { 
                    Console.WriteLine(Ex.Message);//сообщение о неправильном ID продукта
                    return;
                }
            }
            List<Product> prodSumm = new();//создаем новый список
            for (int i = 0; i < idProd.Length; i++)
            {
                if (product.Exists(x => x.productID == idProd[i]))
                {   //добавляем в список соответствующие товары с ID  из массива
                     prodSumm.Add(product.Find(x => x.productID == idProd[i]));

                }
                else
                {
                    Console.WriteLine($"Товара с ID={idProd[i]} в списке нет....");
                    return;
                }

            }
            PrintSheet(prodSumm);
        }
        // удаляет продукты
        public void DeleteProduсtList(List<Product> product, string strKitsummId)
        {
            string[] str = strKitsummId.Trim('-').Split('-');//закидываем в массив строку из символов
            int[] idProd = new int[str.Length];
            for (int i = 0; i < str.Length; i++)//в массив int копируем строчный массив
            {
                try
                {
                    idProd[i] = int.Parse(str[i]);
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);//сообщение о неправильном ID продукта
                    return;
                }
            }
            if(idProd.Length >1)
            {
                for (int i = 0; i < idProd.Length - 1; i++)
                {
                    for (int j = i+1; j < idProd.Length; j++)
                    {
                        if (idProd[i] == idProd[j])
                        {
                            Console.WriteLine("Повторяется ID продукта!!!\nПродолжить ALT+D.");
                            return;
                        }
                    }
                }
            }
          

            for (int i = 0; i < idProd.Length; i++)
            {
                if (!product.Exists(x => x.productID == idProd[i]))
                {   
                   Console.WriteLine($"Товара с ID={idProd[i]} в списке нет....\n" +
                       $"Продолжить ALT+D");
                    return;
                }
            }

            List<Product> prodSumm = new();//создаем новый список
            for (int i = 0; i < idProd.Length; i++)
            {
                prodSumm.Add(product.Find(x => x.productID == idProd[i]));

                product.RemoveAll(x => x.productID == idProd[i]);
            }
            Console.WriteLine($"Вы удалили продукты:");
            PrintSheet(prodSumm);
            Console.WriteLine("Осталось продуктов  в списке....");
            PrintSheet(product);
            Console.WriteLine("Сохранить изменеия ALT+S.\nОтменить изменения ALT+L.");
        }



    }

        
   
}
