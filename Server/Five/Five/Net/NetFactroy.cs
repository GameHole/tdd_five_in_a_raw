using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Five
{
    public class NetFactroy
    {
        private SerializerRegister register;

        public NetFactroy(SerializerRegister register)
        {
            this.register = register;
        }

        public TcpSocket NewClient()
        {
            var socket = new NetTcpSocket();
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            var client = new TcpSocket(socket, register);
            return client;
        }

        public Server NewServer(string ip, int port, ProcesserFactroy factroy)
        {
            var net = new NetTcpSocket();
            net.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            return new Server(net, factroy);
        }
    }
}
