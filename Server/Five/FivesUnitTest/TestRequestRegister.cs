using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestRequestRegister
    {
        Client client;
        [SetUp]
        public void SetUp()
        {
            client = new Client(new LogSocket(), new Matching());
            new RequestRegister().Regist(client);
        }
        [Test]
        public void testRegistedProcess()
        {
            Assert.IsTrue(client.processer.Processers.Contains(MessageCode.RequestMatch));
            Assert.IsTrue(client.processer.Processers.Contains(MessageCode.RequestCancelMatch));
            Assert.IsTrue(client.processer.Processers.Contains(MessageCode.RequestPlay));
        }
    }
}
