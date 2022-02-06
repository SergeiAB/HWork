using System;


namespace Observer
{

    public class Program
    {
        static void Main(string[] args)
        {
            StoreManager storeManager = new ("Ромашка");
            ClientStore client1 = new ("Сергей", "qwer@mail.ru");
            ClientStore client2 = new("Иван", "asd@mail.ru");
            client2.RegistrProduct(storeManager);
            client1.RegistrProduct(storeManager);
            storeManager.SimulateNewProduct("яблоки", 25.5m, "очень вкусные");
            storeManager.SimulateNewProduct("груши", 12.9m, "как лампочка");
            client2.UnregistrProduct(storeManager);
            client1.UnregistrProduct(storeManager);

            Console.ReadKey();
        }
    }
}
