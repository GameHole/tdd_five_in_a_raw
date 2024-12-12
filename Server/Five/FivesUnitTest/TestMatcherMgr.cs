using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesUnitTest
{
    class TestMatcherMgr
    {
        private TIdGenrator gen;
        PlayerRepository mgr;
        [SetUp]
        public void SetUp()
        {
            gen = new TIdGenrator() { id=2,inviled=1 };
            mgr = new PlayerRepository(gen);
        }
        [Test]
        public void testClientMgr()
        {
            Assert.AreEqual(0, mgr.Count);
        }
        [Test]
        public void testonAcceptMgr()
        {
            var socket = new LogSocket();
            mgr.Login(socket);
            Assert.AreEqual(2, socket.Id);
            Assert.AreEqual(1, mgr.Count);
            var player = mgr.FindPlayer(socket);
            Assert.AreSame(socket, player.notifier);

            socket.onClose.Invoke();
            Assert.AreEqual(typeof(NoneNotifier), player.notifier.GetType());
            Assert.AreEqual(0, mgr.Count);
            Assert.AreEqual(1, socket.Id);
        }
        [Test]
        public void testIdGenrator()
        {
            var genrator = new IdGenrator();
            Assert.AreEqual(0, genrator.InvailedId);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(i + 1,genrator.Genrate());
            }
        }
        [Test]
        public void testSocketClose()
        {
            var scket = new LogSocket();
            mgr.Login(scket);
            scket.Close();
            Assert.AreEqual(0, mgr.Count);
        }
    }
}
