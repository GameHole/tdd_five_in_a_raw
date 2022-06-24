using Five;
using System.Net.Sockets;

namespace FivesUnitTest
{
    class LogSocket : ASocket
    {
        internal string log;
        public override void Send(Message message)
        {
            log = "Send " + message.ToString();
        }
    }
}