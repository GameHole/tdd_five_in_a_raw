using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TAccepter : IApp
    {
        public TcpSocket ssocket = null;
        internal int stopCount;

        public void Invoke(ASocket socket)
        {
            ssocket = socket as TcpSocket;
        }

        public void Stop()
        {
            stopCount++;
        }
    }
    class TestServer
    {

        [Test]
        public async Task testServer()
        {
            var accepter = new TAccepter();
            var server = new Server("127.0.0.1", TestApp.port, accepter);
            server.StartAsync();
            await Task.Delay(200);
            Assert.IsTrue(server.IsRun);
            var client = new TcpSocket(new SerializerRegister());
            client.Connect("127.0.0.1", TestApp.port);
            await Task.Delay(200);
            Assert.AreEqual(1, server.sockets.Count);
            server.Stop();
            Assert.AreEqual(1, accepter.stopCount);
            Assert.IsFalse(server.IsRun);
            Assert.AreEqual(0, server.sockets.Count);
            Assert.IsFalse(accepter.ssocket.isVailed);
            Assert.Throws<ObjectDisposedException>(() =>
            {
                server.socket.Accept();
            });
        }
    }
}
