using Five;
using System.Net.Sockets;

namespace FivesUnitTest
{
    public class LogSocket : AClient
    {
        internal string log;

        public override void Send(Message message)
        {
            log = "Send " + message.ToString();
        }
    }
}