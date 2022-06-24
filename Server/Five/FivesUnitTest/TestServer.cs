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
        public async Task testStart()
        {
            var server = new Server("127.0.0.1", 10000);
            Assert.AreNotEqual(null, server.Listener);
            server.Start();
            await Task.Delay(200);
            var client = new TcpClient();
            client.Connect("127.0.0.1", 10000);
            await Task.Delay(200);
            Assert.AreEqual(1, server.clients.Count);
            var sclient = server.clients[0];
            server.Stop();
            Assert.AreEqual(0, server.clients.Count);
            Assert.Throws<ObjectDisposedException>(() =>
            {
                sclient.GetStream();
            });
        }
    }
}
