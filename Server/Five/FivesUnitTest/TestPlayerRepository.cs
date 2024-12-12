using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesUnitTest
{
    class TestPlayerRepository
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
        public void testNew()
        {
            Assert.AreEqual(0, mgr.Count);
        }
        [Test]
        public void testOnAcceptMgr()
        {
            var socket = new LogSocket();
            mgr.Login(socket);
            Assert.AreEqual(2, socket.Id);
            
            Assert.AreEqual(1, mgr.Count);
            var player = mgr.FindPlayer(socket.Id);
            Assert.AreSame(socket, player.notifier);
            Assert.AreEqual(2, player.Id);
            mgr.OutLine(socket.Id);
            Assert.AreEqual(typeof(NoneNotifier), player.notifier.GetType());
            Assert.AreEqual(0, mgr.Count);
            Assert.AreEqual(2, socket.Id);
        }
        [Test]
        public void testNewPlayer()
        {
            var player = mgr.NewPlayer();
            Assert.IsInstanceOf<NoneNotifier>(player.notifier);
            Assert.AreEqual(2, player.Id);
            Assert.AreEqual(1, mgr.Count);
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
