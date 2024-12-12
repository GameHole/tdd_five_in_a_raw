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
            notifier = new NetNotifier(socket,new Player());
        }
        [Test]
        public void testStart()
        {
            var infos = new PlayerInfo[] { new PlayerInfo(1, 10), new PlayerInfo(2, 20) };
            notifier.Start(new StartNotify { playerId = -1, infos = infos });
            Assert.AreEqual("Send opcode:7 -1 (1,10)(2,20)", socket.log);
        }
        [Test]
        public void testFinish()
        {
            notifier.Finish(new PlayerIdNotify(11, 1));
            Assert.AreEqual("Send opcode:11 1", socket.log);
        }
        [Test]
        public void testPlayed()
        {
            notifier.Played(new PlayedNotify { x = 5, y = 6, id = 1 });
            Assert.AreEqual("Send opcode:9 (5,6)1", socket.log);
        }
        [Test]
        public void testTurn()
        {
            notifier.Turn(new PlayerIdNotify { opcode=13, playerId=1});
            Assert.AreEqual("Send opcode:13 1", socket.log);
        }
    }
}
