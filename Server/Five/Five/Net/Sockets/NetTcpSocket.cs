using System;
using System.Net;
using System.Net.Sockets;

namespace Five
{
    public abstract class ANetSocket
    {
        public Socket socket { get; protected set; }
        public abstract void Bind(IPEndPoint iPEndPoint);
        public abstract void Listen(int backlog);
        public abstract int Receive(byte[] bytes, int offset, int size, SocketFlags flags);
        public abstract void Send(byte[] bytes, int offset, int size, SocketFlags flags);
        public abstract void Connect(string ip, int port);
        public abstract void Disconnect(bool reuseSocket);
        public abstract ANetSocket Accept();

        public void Close()
        {
            socket.Close();
        }
    }
    public class NetTcpSocket: ANetSocket
    {
        public NetTcpSocket():this(new Socket(SocketType.Stream, ProtocolType.Tcp))
        {}

        public NetTcpSocket(Socket socket)
        {
            this.socket = socket;
        }

        public override void Bind(IPEndPoint iPEndPoint)
        {
            socket.Bind(iPEndPoint);
        }
        public override void Listen(int backlog)
        {
            socket.Listen(backlog);
        }

        public override ANetSocket Accept()
        {
            return new NetTcpSocket(socket.Accept());
        }

        public override int Receive(byte[] bytes, int offset, int size, SocketFlags flags)
        {
            return socket.Receive(bytes, offset, size, flags);
        }

        public override void Send(byte[] bytes, int offset, int size, SocketFlags flags)
        {
            socket.Send(bytes, offset, size, flags);
        }

        public override void Connect(string ip, int port)
        {
            socket.Connect(ip, port);
        }

        public override void Disconnect(bool reuseSocket)
        {
            socket.Disconnect(reuseSocket);
        }
    }
}
