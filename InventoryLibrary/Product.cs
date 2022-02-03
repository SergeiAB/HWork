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
        private int _productID;
        public string nameProduct;
        public decimal unitPrice;
        public double numberUnits;

        //[JsonConstructor]
        public Product(string nameProduct, decimal unitPrice, double numberUnits, int productID)
        {
            this.nameProduct = nameProduct;
            this.unitPrice = unitPrice;
            this.numberUnits = numberUnits;
            _productID = productID;
        }

        public int productID
        {
            get { return _productID; }
            private set {_productID = value < 1 ? 1:value;}
        }

        //стоимость в одной позиции товара
        public decimal AmountPrice()
        {
            return unitPrice * (decimal)numberUnits;
        }

       
        //переопределения методов
        public override string ToString()
        {
            decimal amount = AmountPrice();
            string str = String.Format("| {0,-5} | {1,-15} | {2,-10} | {3,-12} | {4,12} |", productID, nameProduct, numberUnits, unitPrice.ToString("c2"),amount.ToString("c2"));
            return str;
        }
        //переопределяем методы и операторы сравнения
        public override int GetHashCode()
        {
            return productID;
        }

        public bool Equals(Product prod)
        {
            if (prod == null) return false;
            Product tmp = prod as Product;
            return productID == tmp.productID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            int tmp = (int)obj;
            return productID == tmp;
        }

        public static bool operator == (Product product, int Id)
        {
            return product.productID == Id;
        }

        public static bool operator != (Product product, int Id)
        {
            return !(product.productID == Id);
        }


    }


   
    
}
