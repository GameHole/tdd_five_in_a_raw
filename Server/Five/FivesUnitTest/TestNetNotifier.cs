using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Five;
namespace FivesUnitTest
{
    class TestNetNotifier
    {
        LogSocket socket;
        NetNotifier notifier;
        [SetUp]
        public void SetUp()
        {
            socket = new LogSocket();
            notifier = new NetNotifier(socket);
        }
        [Test]
        public void testStart()
        {
            notifier.Start(new PlayerInfo[] { new PlayerInfo(1, 10), new PlayerInfo(2, 20) });
            Assert.AreEqual("Send opcode:7 (1,10)(2,20)", socket.log);
        }
        [Test]
        public void testFinish()
        {
            notifier.Finish(1);
            Assert.AreEqual("Send opcode:11 1", socket.log);
        }
        [Test]
        public void testPlayed()
        {
            notifier.Played(5,6,1);
            Assert.AreEqual("Send opcode:9 (5,6)1", socket.log);
        }
    }
}
