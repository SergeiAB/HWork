using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverInterfase
{
    internal class MessageMail
    {
        private string _toWhom;
        private string _fromWhom;
        private string _message;
        public string toWhom { get { return _toWhom; } set { _toWhom=value;} }
        public string fromWhom { get {return _fromWhom;} set {_fromWhom=value;}}
        public string message { get {return _message;} set {_message=value;} }
        
        public MessageMail(string toWhom,string fromWhom, string message)
        {
            _toWhom = toWhom;
            _fromWhom = fromWhom; 
            _message = message;
        }
    }
}
