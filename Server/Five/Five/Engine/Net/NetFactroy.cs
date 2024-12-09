using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Five
{
    public class SocketFactroy
    {
        public ASocket FactroySocket()
        {
            return new TcpSocket();
        }
    }
    public class NetFactroy
    {
        private SerializerRegister register;
        private SocketFactroy sockets;

        public NetFactroy(SerializerRegister register, SocketFactroy sockets)
        {
            this.register = register;
            this.sockets = sockets;
        }

        public Client NewClient()
        {
            var socket = sockets.FactroySocket();
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            var serializer = new MessageSerializer();
            register.Regist(serializer);
            var client = new Client(socket, serializer);
            return client;
        }

        private ASocket FactroySocket()
        {
            return new TcpSocket();
        }

        public Server NewServer(string ip, int port, IProcesserFactroy factroy)
        {
            var net = sockets.FactroySocket();
            net.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            var serializer = new MessageSerializer();
            register.Regist(serializer);
            return new Server(net, factroy.Factroy(), serializer);
        }
    }
}
