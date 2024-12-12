using Five;
using System.Net.Sockets;

namespace FivesUnitTest
{
    public class LogSocket : AClient
    {
        internal string log;
        public override void Close()
        {
            onClose?.Invoke();
        }

        public override void Send(Message message)
        {
            log = "Send " + message.ToString();
        }
    }
    public class TClient : AClient
    {
        public int _id;

        public override int Id =>_id;
        public TClient()
        {
            _id = GetHashCode();
        }
        public override void Close() { }

        public override void Send(Message message) { }
    }
}