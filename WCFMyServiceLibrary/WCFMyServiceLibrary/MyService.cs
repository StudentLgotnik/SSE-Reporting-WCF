using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFMyServiceLibrary
{
    public class MyService : IMyService
    {
        public string SendEmplMessage(string from, string message, string to)
        {
            string mess = from + ": " + message + " -> " + to;
            return mess;
        }

        public string SendMessage(string from, string message)
        {
            string mess = from + ": " + message;
            return mess;
        }
    }
}
