﻿using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    public class TGameFactroy : IGameFactroy
    {
       public TGame game= new TGame();
        public AGame Factroy()
        {
            return game;
        }
    }
    class TAccepter : Domain
    {
        internal int stopCount;

        public TAccepter() : base(new TGameFactroy(),new TIdGenrator())
        {
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
        private Five.Client client;

        [SetUp]
        public void set()
        {
            var factroy = new NetFactroy(new SerializerRegister(),new SocketFactroy());
            accepter = new TAccepter();
            log = new LogRequestRegister(accepter);
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
            Assert.AreEqual(1, server.clients.Count);
            var ssocket = server.clients.First();
            server.Stop();
            Assert.AreEqual(1, accepter.stopCount);
            Assert.IsFalse(server.IsRun);
            Assert.AreEqual(0, server.clients.Count);
            Assert.IsFalse(ssocket.isVailed);
            Assert.Throws<ObjectDisposedException>(() =>
            {
                server.socket.Accept();
            });
        }
        [Test]
        public void testSocket()
        {
            Assert.IsInstanceOf<TcpSocket>(server.socket);
        }
    }
}
