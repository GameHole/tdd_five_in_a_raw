using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TestServer
    {
        [Test]
        public async Task testServer()
        {
            var server = new Server("127.0.0.1", 10000);
            TcpSocket ssocket = null;
            server.onAccept += (socket) =>
            {
                ssocket = socket;
            };
            server.StartAsync();
            await Task.Delay(200);
            Assert.IsTrue(server.IsRun);
            var client = new TcpSocket(new SerializerRegister());
            client.Connect("127.0.0.1", 10000);
            await Task.Delay(200);
            Assert.AreEqual(1, server.sockets.Count);
            server.Stop();
            Assert.IsFalse(server.IsRun);
            Assert.AreEqual(0, server.sockets.Count);
            Assert.IsFalse(ssocket.isVailed);
            Assert.Throws<ObjectDisposedException>(() =>
            {
                server.socket.Accept();
            });
        }
    }
}
