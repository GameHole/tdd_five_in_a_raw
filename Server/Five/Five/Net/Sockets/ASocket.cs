using System.Net;
using System.Net.Sockets;

namespace Five
{
    public abstract class ASocket
    {
        public Socket socket { get; protected set; }
        public abstract void Bind(IPEndPoint iPEndPoint);
        public abstract void Listen(int backlog);
        public abstract int Receive(byte[] bytes, int offset, int size, SocketFlags flags);
        public abstract void Send(byte[] bytes, int offset, int size, SocketFlags flags);
        public abstract void Connect(string ip, int port);
        public abstract void Disconnect(bool reuseSocket);
        public abstract ASocket Accept();

        public void Close()
        {
            socket.Close();
        }
    }
}
