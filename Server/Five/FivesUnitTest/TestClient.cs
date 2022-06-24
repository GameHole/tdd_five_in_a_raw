using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestClient
    {
        LogSocket logSocket;
        LogMatcher logMatcher;
        LogPlayer logPlayer;
        LogClient client;
        [SetUp]
        public void SetUp()
        {
            logSocket = new LogSocket();
            logMatcher = new LogMatcher(new Matching());
            logPlayer = new LogPlayer();
            logMatcher.Player = logPlayer;
            client = new LogClient(logSocket, logMatcher);
        }
        [Test]
        public void testProcessMessage()
        {

            client.Process(new Message(MessageCode.RequestMatch));

            Assert.AreEqual("Match ", logMatcher.log);
            Assert.AreEqual("Send opcode:2 result:0", logSocket.log);

            client.Process(new Message(MessageCode.RequestCancelMatch));

            Assert.AreEqual("Match CancelMatch ", logMatcher.log);
            Assert.AreEqual("Send opcode:4 result:0", logSocket.log);

        }
        [Test]
        public void testPlayApi()
        {
            var message = new PlayMessage(1, 2);
            client.Process(message);

            Assert.AreEqual($"Play({message.x},{message.y}) ", logPlayer.log);
            Assert.AreEqual("Send opcode:6 result:29999", logSocket.log);
        }
        [Test]
        public void testPlayApiException()
        {
            var ex = Assert.Throws<InvalidCastException>(() =>
            {
                client.Process(new Message(MessageCode.RequestPlay));
            });
        }
        [Test]
        public void testProcessException()
        {
            Assert.DoesNotThrow(() =>
            {
                client.Process(new Message(999));
            });
        }
        [Test]
        public void testRecv()
        {
            logSocket.onRecv(new Message(1));
            Assert.AreEqual("Process", client.log);
        }
    }
}
