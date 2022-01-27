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
        public void PrintSheet(List<Product> product, bool total=true, bool title =true)
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


        //удаление из списка по выбранному ID товара
        public void DeleteProduct(List<Product> product,int DeleteProdID)
        {
            if (product.Exists(x => x.ProductID == DeleteProdID))
            {
                var print = product.FindAll(x => x.ProductID == DeleteProdID);
                Console.WriteLine($"Вы удалили Продукт с ID= {DeleteProdID}");
                PrintSheet(print,false,false);
                product.RemoveAll(x => x.ProductID == DeleteProdID);
            }
            else
            {
                Console.WriteLine($"Продукта с ID= {DeleteProdID} в списке нет!\nДля удаления выберите CTRL+D.");
            }
            

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
        
        //сумирует товары
        public void SummProdukt(List<Product> product, string strKitsummId)
        {       
            string[] str = strKitsummId.Trim('+').Split('+');
            int[] IDprod = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                try
                {
                    IDprod[i] = int.Parse(str[i]);
                }
                catch(Exception Ex) 
                { 
                    Console.WriteLine(Ex.Message);
                    return;
                }
            }
            List<Product> ProdSumm = new List<Product>();
            for (int i = 0; i < IDprod.Length; i++)
            {
                if (product.Exists(x => x.ProductID == IDprod[i]))
                {
                     ProdSumm.Add(product.Find(x => x.ProductID == IDprod[i]));

                }
                else
                {
                    Console.WriteLine($"Товара с ID={IDprod[i]} в списке нет....");
                    return;
                }

            }
            PrintSheet(ProdSumm);
        }

        




    }

        
   
}
