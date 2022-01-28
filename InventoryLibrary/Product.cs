using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public class Product
    {
        private int _ProductID;
        public string NameProduct;
        public decimal UnitPrice;
        public double NumberUnits;

        //[JsonConstructor]
        public Product(string NameProduct, decimal UnitPrice, double NumberUnits, int ProductID)
        {
            this.NameProduct = NameProduct;
            this.UnitPrice = UnitPrice;
            this.NumberUnits = NumberUnits;
            _ProductID = ProductID;
        }

        public int ProductID
        {
            get { return _ProductID; }
            private set {_ProductID = value < 1 ? 1:value;}
        }

        //стоимость в одной позиции товара
        public decimal AmountPrice()
        {
            return UnitPrice * (decimal)NumberUnits;
        }

            // Выбирает максимальный ProductID из списка и увеличивает на 1
       

        //переопределения методов
        public override string ToString()
        {
            decimal Amount = AmountPrice();
            string str = String.Format("| {0,-5} | {1,-15} | {2,-10} | {3,-12} | {4,12} |", ProductID, NameProduct, NumberUnits, UnitPrice.ToString("c2"),Amount.ToString("c2"));
            return str;
        }
        //переопределяем методы и операторы сравнения
        public override int GetHashCode()
        {
            return ProductID;
        }

        public bool Equals(Product prod)
        {
            if (prod == null) return false;
            Product tmp = prod as Product;
            return ProductID == tmp.ProductID;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    int tmp = (int)obj;
        //    return ProductID == tmp;
        //}

        public static bool operator == (Product product, int Id)
        {
            return product.ProductID == Id;
        }

        public static bool operator != (Product product, int Id)
        {
            return !(product.ProductID == Id);
        }


    }


   
    
}
