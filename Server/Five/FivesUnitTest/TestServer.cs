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
    class TAccepter : App
    {
        public Five.DefaultClient ssocket = null;
        internal int stopCount;

        public TAccepter(GameFactroy factroy) : base(factroy)
        {
        }

        public override void Login(AClient socket)
        {
            ssocket = socket as Five.DefaultClient;
        }

        public override void Stop()
        {
            stopCount++;
        }
    }
    class TestServer
    {
        private TAccepter accepter;
        private LogRequestRegister log;
        private Server server;
        private Five.DefaultClient client;

        [SetUp]
        public void set()
        {
            var factroy = new NetFactroy(new SerializerRegister());
            accepter = new TAccepter(new GameFactroy());
            log = new LogRequestRegister(new MatchServce( accepter));
            server = factroy.NewServer("127.0.0.1", TestApp.port, log);
            client = factroy.NewClient();
        }
        [TearDown]
        public void tear()
        {
            server.Stop();
        }
        [Test]
        public async Task testServer()
        {
            server.StartAsync();
            await Task.Delay(200);
            Assert.IsTrue(server.IsRun);
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
        [Test]
        public void testSocket()
        {
            Assert.IsInstanceOf<NetTcpSocket>(server.socket);
        }
    }
}
