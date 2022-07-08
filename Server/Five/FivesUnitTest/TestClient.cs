using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestClient
    {
        [Test]
        public void testClient()
        {
            var client = new Client(new LogSocket(), new Matching());
            Assert.NotNull(client.matcher);
            Assert.NotNull(client.processer);
            Assert.NotNull(client.socket);
        }
        [Test]
        public void testNotifier()
        {
            var client = new Client(new LogSocket(), new Matching());
            Assert.AreEqual(typeof(NetNotifier), client.matcher.Player.notifier.GetType());
        }
    }
}
