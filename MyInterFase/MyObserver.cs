using System;


namespace MyInterFase
{
    internal class MyObserver : IMyObserver
    {
        public string Name { get; private set; }
        public MyObserver(string name)
        {
            Name = name;
        }

        public void ResponseToEvent(object sender, EventArgs e)
        {
            Console.WriteLine($" {this} Вас оповещает {sender} что произошло событие!!!!!");
        }

        public void SubscribeEvent(IMyObservable observable)
        {
            observable.OnEvent+=ResponseToEvent;
            Console.WriteLine($" Клиент {this} подписался на {observable}");
        }

        public void UnsubscribeEvent(IMyObservable observable)
        {
            observable.OnEvent-=ResponseToEvent;
            Console.WriteLine($" Клиент {this} отписался от {observable}");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
