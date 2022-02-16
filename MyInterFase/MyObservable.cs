using System;


namespace MyInterFase
{
    internal class MyObservable : IMyObservable
    {
        public string Name { get; private set; }
        public event EventHandler OnEvent;
        
        public MyObservable(string name)
        {
            Name = name;
        }

        public void AnEventHappened()
        {
            OnEvent.Invoke(this,EventArgs.Empty);
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
