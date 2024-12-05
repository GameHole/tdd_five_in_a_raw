using System;
using System.Net;

namespace Five
{
    class Program
    {
        static void Main(string[] args)
        {
            var factroy = new ProcesserFactroy(new App());
            var socket = new NetTcpSocket();
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000));
            var server = new Server(socket, factroy);
            server.Start();
        }
    }
}
