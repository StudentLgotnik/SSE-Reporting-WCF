using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCFMyServiceLibrary
{
    [ServiceContract]
    public interface IMyService
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        [OperationContract]
        string SendMessage(string from, string to);
        /// <summary>
        /// Sends the empl message.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="message">The message.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        [OperationContract]
        string SendEmplMessage(string from, string message, string to);
    }
}
