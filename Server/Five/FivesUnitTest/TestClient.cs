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
            client = new Client();
            client.Init(new LogSocket());
        }
        [Test]
        public void testClient()
        {
            Assert.NotNull(client.processer);
            Assert.NotNull(client.socket);
        }
    }
}
