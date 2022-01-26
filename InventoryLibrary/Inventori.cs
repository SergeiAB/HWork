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
        public void PrintSheet(List<Product> product)
        {
            Console.WriteLine(new String('-', 70));
            Console.WriteLine("| {0,-5} | {1,-15} | {2,-10} | {3,-12} | {4,-12} |", "Инв №", "Наименование", "Кол-во ед.", "Цена за ед.", "Всего:");
            Console.WriteLine(new String('-', 70));
            foreach (Product prod in product)
            {
                Console.WriteLine(prod.ToString());
                Console.WriteLine(new String('-', 70));
            }
            decimal total = TotalPrice(product);
            Console.WriteLine("|{0,52} | {1,12} |", "Итого:", total.ToString("c2"));
            Console.WriteLine(new String('-', 70));
        }

        //удаление из списка по выбранному ID товара
        public void DeleteProduct(List<Product> product,int IDProduct)
        {
            product.RemoveAll(x => x.ProductID == IDProduct);

        }

        //возвращает индекс в листе продукта по его ID
        public int GetIndex(List<Product> product,int IDProduct)
        {
            int index;
            for (index = 0; index < product.Count; index++)
            {
                if (product[index].ProductID == IDProduct)
                {
                    break;
                }
            }
            return index;
        }

        




    }

        
   
}
