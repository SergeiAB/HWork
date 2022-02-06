using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Observer
{
    public class StoreManager//интернет магазин 
    {

        private string _storeName;
        public string storeName { get { return _storeName; } }
        public StoreManager(string storeName)
        {
            _storeName = storeName;
        }

        public event EventHandler<ComingProduct> NewProduct;//событие поступления товара

        protected virtual void OnNewProduct(ComingProduct e)//метод для уведомления зарегистрированных объектов
        {
            //сохранить ссылку на делегата в временной переменной
            //для обеспечения безопасности потоков
            EventHandler<ComingProduct> temp = Volatile.Read(ref NewProduct);
            temp?.Invoke(this, e);//вызов события 
        }
        public override string ToString()
        {
            return storeName;
        }
        public void SimulateNewProduct(string nameProduct, decimal priceProduct, string descriptionProduc)
        {
            ComingProduct e = new(nameProduct, priceProduct, descriptionProduc);

            if (NewProduct != null)
            {
                OnNewProduct(e);
            }
        }
    }
}
