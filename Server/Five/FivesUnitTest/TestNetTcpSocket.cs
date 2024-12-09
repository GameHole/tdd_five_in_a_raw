using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    internal class TestNetTcpSocket
    {
        private TcpSocket test;
        private IPEndPoint remote;

        [SetUp]
        public void set()
        {
            test = new TcpSocket();
            remote = new IPEndPoint(IPAddress.Parse("127.0.0.1"), TestApp.port);
            test.Bind(remote);
        }
        [TearDown]
        public void tear()
        {
            test.Close();
        }
        [Test]
        public async Task testAccept()
        {
            test.Listen(1);
            TcpSocket acc = null;
            Task task = Task.Factory.StartNew(() =>
            {
                acc = test.Accept() as TcpSocket;
            });
            await Task.Delay(200);
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            socket.Connect(remote);
            await Task.Delay(200);
            Assert.NotNull(acc);
        }
    }
}
