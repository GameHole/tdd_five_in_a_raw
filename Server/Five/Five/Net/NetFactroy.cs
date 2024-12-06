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

        public DefaultClient NewClient()
        {
            var socket = new NetTcpSocket();
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            var serializer = new MessageSerializer();
            register.Regist(serializer);
            var client = new DefaultClient(socket, serializer);
            return client;
        }

        public Server NewServer(string ip, int port, ProcesserFactroy factroy)
        {
            var net = new NetTcpSocket();
            net.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            var serializer = new MessageSerializer();
            register.Regist(serializer);
            return new Server(net, factroy, serializer);
        }
    }
}
