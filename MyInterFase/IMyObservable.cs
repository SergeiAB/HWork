using System;


namespace MyInterFase
{
    internal interface IMyObservable
    {
        event EventHandler OnEvent;//событие 
        void AnEventHappened();//метод для вызова события
    }
}
