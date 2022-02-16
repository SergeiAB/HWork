using System;

namespace MyInterFase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyObservable provaider1 = new("Провайдер1");
            MyObservable provaider2 = new("Провайдер2");
            MyObserver client1 = new("Пользователь1");
            client1.SubscribeEvent(provaider1);
            MyObserver client2 = new("Пользователь2");
            client2.SubscribeEvent(provaider1);
            MyObserver client3 = new("Пользователь3");
            client3.SubscribeEvent(provaider1);
            provaider1.AnEventHappened();
            Console.WriteLine();
            client2.UnsubscribeEvent(provaider1);
            provaider1.AnEventHappened();
            Console.WriteLine();
            client3.UnsubscribeEvent(provaider1);
            client2.SubscribeEvent(provaider2);
            provaider1.AnEventHappened();
            provaider2.AnEventHappened();
            Console.ReadLine();
        }
    }
}
