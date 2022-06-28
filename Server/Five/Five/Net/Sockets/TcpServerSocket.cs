using System.Net.Sockets;

namespace Five
{
    class TcpServerSocket:TcpSocket
    {
        private Server server;
        public TcpServerSocket(Socket socket, Server server):base(socket)
        {
            this.server = server;
            RecvAsync();
        }
        protected override void CloseInternal()
        {
            base.CloseInternal();
            server.sockets.Remove(this);
        }
    }
}