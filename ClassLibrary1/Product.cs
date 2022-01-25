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

        public Product(string NameProduct, decimal UnitPrice, double NumberUnits, List<Product> ListProducts)
        {
            this.NameProduct = NameProduct;
            this.UnitPrice = UnitPrice;
            this.NumberUnits = NumberUnits;
            _ProductID = GetID(ListProducts);
        }

        public int ProductID
        {
            get { return _ProductID; }
            private set { _ProductID = value; }
        }

        //стоимость в одной позиции товара
        public decimal AmountPrice()
        {
            return UnitPrice * (decimal)NumberUnits;
        }

            // Выбирает максимальный ProductID из списка и увеличивает на 1
        private int GetID(List<Product> products)
        {   
        int i=0;
        int id=1;
            if (products.Count > 0)
            {
                int[] ArrayID = new int[products.Count];
                foreach(Product prd in products)
                {
                    ArrayID[i] = prd.ProductID;
                        i++;
                }
                id = ArrayID.Max()+1;
            }
        return id;

        }
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

        //public bool Equals(Product prod)
        //{
        //    if (prod == null) return false;
        //    Product tmp = prod as Product;
        //    return ProductID == tmp.ProductID;
        //}

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            int tmp = (int)obj;
            return ProductID == tmp;
        }

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
