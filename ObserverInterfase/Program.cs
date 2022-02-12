using System;

namespace ObserverInterfase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MailService mailService = new MailService();
            ClientMail clientPol = new ClientMail("Pol");
            ClientMail clientBob = new ClientMail("Bob");
            ClientMail clientAnny = new ClientMail("Anny");
            clientPol.Subscribe(mailService);
            clientBob.Subscribe(mailService);
            clientAnny.Subscribe(mailService);

            mailService.MessageClient(new MessageMail("Pol", "Bob", "Как твои дела?"));
            mailService.MessageClient(new MessageMail("Bob", "Pol", "Привет, жду тебя у магазина."));
            mailService.MessageClient(new MessageMail("Anny", "Ivan", "Выходи на улицу."));
            mailService.MessageClient(new MessageMail("Pol", "Bob", "Я в магазине"));
            //clientBob.Unsubscribe();
            clientPol.Unsubscribe();
            //clientAnny.Unsubscribe();

            mailService.MessageClient(new MessageMail("Всех", "mail", "проверка подписки."));
            mailService.MessageClient(new MessageMail("Anny", "Ivan", "Я сильно жду."));
            Console.ReadLine();
        }
    }
}
