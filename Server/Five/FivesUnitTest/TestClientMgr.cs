using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesUnitTest
{
    class TestClientMgr
    {
        ClientMgr mgr;
        [SetUp]
        public void SetUp()
        {
            mgr = new ClientMgr(new Matching());
        }
        [Test]
        public void testClientMgr()
        {
            Assert.AreEqual(0, mgr.clients.Count);
        }
        [Test]
        public void testonAccept()
        {
            var log= new LogRequestRegister();
            mgr.register = log;
            var socket = new LogSocket();
            mgr.Invoke(socket);
            Assert.AreEqual(1, mgr.clients.Count);
            var client = mgr.clients.First();
            Assert.AreSame(client,log.client);
            Assert.AreSame(mgr, log.mgr);
            Assert.IsTrue(client.processer.Processers.Contains(MessageCode.RequestMatch));
            Assert.IsTrue(client.processer.Processers.Contains(MessageCode.RequestCancelMatch));
            Assert.IsTrue(client.processer.Processers.Contains(MessageCode.RequestPlay));
            Assert.NotNull(client.processer);
            Assert.AreSame(socket,client.socket);
            Assert.AreEqual(1, mgr.matchers.Count);
            var matcher = mgr.matchers[socket];
            Assert.AreEqual(typeof(NetNotifier), matcher.Player.notifier.GetType());

            var logP = new LogPlayer();
            matcher.Player = logP;
            socket.onClose.Invoke();
            Assert.AreEqual("OutLine ", logP.log);
        }
        [Test]
        public void testRemove()
        {
            Client client = null;
            mgr.clients.onAdd += (c) => client = c;
            mgr.Invoke(new LogSocket());
            mgr.clients.Remove(client);
            Assert.AreEqual(0, mgr.clients.Count);
        }
        [Test]
        public void testSocketClose()
        {
            var scket = new LogSocket();
            mgr.Invoke(scket);
            scket.Close();
            Assert.AreEqual(0, mgr.clients.Count);
            Assert.AreEqual(0, mgr.matchers.Count);
        }
    }
}
