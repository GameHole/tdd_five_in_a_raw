using Five;

namespace UnitTests
{
    internal class ErrorSocket:ASocket
    {
        public ErrorSocket()
        {
        }

        public override void Close()
        {
            throw new System.NotImplementedException();
        }

        public override void Connect(string ip, int port)
        {
            throw new System.Net.Sockets.SocketException();
        }

        public override void Send(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}