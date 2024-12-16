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
        PlayerRepository rsp;
        private LoginServce servce;

        [SetUp]
        public void SetUp()
        {
            gen = new TIdGenrator() { id=2,inviled=1 };
            rsp = new PlayerRepository();
            servce = new LoginServce(rsp, gen);
        }
        [Test]
        public void testNew()
        {
            Assert.AreEqual(0, rsp.Count);
        }
        [Test]
        public void testNewPlayer()
        {
            var ntf = new NoneNotifier();
            var playerId = servce.Login(ntf);
            var player = rsp.FindPlayer(playerId);
            Assert.AreSame(ntf,player.notifier);
            Assert.AreEqual(2, player.Id);
            Assert.AreEqual(1, rsp.Count);
            servce.OutLine(playerId);
            Assert.AreEqual(typeof(NoneNotifier), player.notifier.GetType());
            Assert.AreEqual(0, rsp.Count);
        }
        [Test]
        public void testAdd()
        {
            var player = new Player(2);
            rsp.Add(player);
            Assert.AreEqual(1, rsp.Count);
            Assert.AreSame(player, rsp.FindPlayer(2));
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
    }
}
