using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverInterfase
{
    internal class ClientMail : IObserver<MessageMail>
    {
        private string _name;

        public string Name => _name;
       
        public ClientMail(string Name)
        {
            _name = Name;
        }

        private IDisposable disposable;//хранится ссылка на объект с методом отписки от события

        void IObserver<MessageMail>.OnCompleted()
        {
            throw new NotImplementedException();
        }

        void IObserver<MessageMail>.OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        void IObserver<MessageMail>.OnNext(MessageMail value)
        {
            Console.WriteLine($"Сообщение для {value.toWhom}! от {value.fromWhom} текст: {value.message}");
        }

        public virtual void Subscribe(IObservable<MessageMail> mailService)// подписка на событие 
        {
            if(mailService != null)
            {
                disposable = mailService.Subscribe(this);
            }
        }
        public void Unsubscribe()//отписка от события
        {
            if(disposable != null)
            {
                disposable.Dispose();
            }
        }
        //переопределение на равенство адреса клиента и сообщения
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is MessageMail mail)
            {
                return Name == mail.toWhom;
            }
            else if (obj is ClientMail other)
            {
                return Name==other.Name;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name);
        }

        public override string ToString()
        {
            return this.Name;
        }
       
    }
}
