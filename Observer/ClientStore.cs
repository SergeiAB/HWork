using System;


namespace Observer
{
    public class ClientStore
    {
        public string _name { get; set; }
        public string _eMail { get; set; }
        public ClientStore(string name, string eMail)
        {
            _name = name;
            _eMail = eMail;
        }
        public void SubscriptionProduct(object sender, ComingProduct e)
        {
            Console.WriteLine($"Уважаемый(ая) {this._name}! магазин \"{sender}\"  получил интересующий Вас :\n" +
                $" {e}.");
            Console.WriteLine();
        }
        public void RegistrProduct(StoreManager manager)
        {
            manager.NewProduct += SubscriptionProduct;
            Console.WriteLine($" {this._name} Вы подписались на товар в магазине \"{manager}\"");
            Console.WriteLine();
        }

        public void UnregistrProduct(StoreManager manager)
        {
            manager.NewProduct -= SubscriptionProduct;
            Console.WriteLine($" {this._name} Вы больше не подписаны на товар в магазине \"{manager}\"");
            Console.WriteLine();

        }


    }
}
