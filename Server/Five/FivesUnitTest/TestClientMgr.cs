using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            mgr.onAccept(new LogSocket());
            Assert.AreEqual(1, mgr.clients.Count);
            Assert.IsTrue(log.isRun);
        }
        [Test]
        public void testRemove()
        {
            Client client = null;
            mgr.clients.onAdd += (c) => client = c;
            mgr.onAccept(new LogSocket());
            mgr.clients.Remove(client);
            Assert.AreEqual(0, mgr.clients.Count);
        }
        [Test]
        public void testSocketClose()
        {
            var scket = new LogSocket();
            mgr.onAccept(scket);
            scket.Close();
            Assert.AreEqual(0, mgr.clients.Count);
        }
    }
}
