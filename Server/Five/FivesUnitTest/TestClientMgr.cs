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
            Assert.AreEqual(0, mgr.ClientCount);
        }
        [Test]
        public void testonAccept()
        {
            mgr.onAccept(new LogSocket());
            Assert.AreEqual(1, mgr.ClientCount);
        }
        [Test]
        public void testRemove()
        {
            mgr.onAccept(new LogSocket());
            mgr.Remove(mgr.GetClient(0));
            Assert.AreEqual(0, mgr.ClientCount);
        }
        [Test]
        public void testSocketClose()
        {
            var scket = new LogSocket();
            mgr.onAccept(scket);
            scket.Close();
            Assert.AreEqual(0, mgr.ClientCount);
        }
    }
}
