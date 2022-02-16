using System;


namespace MyInterFase
{
    internal interface IMyObserver
    {
        void SubscribeEvent(IMyObservable observable);//подписаться на событие
        void UnsubscribeEvent(IMyObservable observable);//отписаться от события
        void ResponseToEvent(object sender, EventArgs e);//метод реагирования на событие

    }
}
