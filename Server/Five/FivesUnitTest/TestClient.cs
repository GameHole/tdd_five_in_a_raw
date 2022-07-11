using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestClient
    {
        Client client;
        [SetUp]
        public void SetUp()
        {
            client = new Client(new LogSocket(), new Matching());
        }
        [Test]
        public void testClient()
        {
            Assert.NotNull(client.matcher);
            Assert.NotNull(client.processer);
            Assert.NotNull(client.socket);
        }
        [Test]
        public void testNotifier()
        {
            Assert.AreEqual(typeof(NetNotifier), client.matcher.Player.notifier.GetType());
        }
        [Test]
        public void testClose()
        {
            var log = new LogPlayer();
            client.matcher.Player = log;
            client.socket.onClose.Invoke();
            Assert.AreEqual("OutLine ", log.log);
        }
    }
}
