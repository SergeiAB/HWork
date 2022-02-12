using System;
using System.Collections.Generic;
using System.Linq;



namespace ObserverInterfase
{
    internal class MailService : IObservable<MessageMail>
    {
        
        List<IObserver<MessageMail>> clientList;
        public MailService()
        {
            clientList = new List<IObserver<MessageMail>>();
        }

        IDisposable IObservable<MessageMail>.Subscribe(IObserver<MessageMail> client)
        {   
            if(!clientList.Contains(client))
            {
                clientList.Add(client);
            }
            return new Unsubscribe(clientList,client);
        }
        private class Unsubscribe : IDisposable
        {
            private IObserver<MessageMail> _client;
            private List<IObserver<MessageMail>> _clientList;

            public Unsubscribe(List<IObserver<MessageMail>> clientList, IObserver<MessageMail> client)
            {
                _client = client;
                _clientList = clientList;
            }

            public void Dispose()
            {
                if (_client != null && _clientList.Contains(_client))
                {
                    Console.WriteLine($"отписаны {_client}");
                    _clientList.Remove(_client);
                }
               
            }


        }

        public void MessageClient( MessageMail mail)
        {
            foreach(var client in clientList)
            {
                if (client.Equals(mail))//если имя клиента совпадает с именем в письме
                {
                    client.OnNext(mail);
                }
                if (mail.toWhom == "Всех")
                {
                    client.OnNext(mail);
                }

            }
        }
    }
}
