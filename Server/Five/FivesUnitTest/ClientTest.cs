using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class ClientTest
    {
        LogSocket logSocket;
        LogMaster log;
        LogPlayer logPlayer;
        Client client;
        [SetUp]
        public void SetUp()
        {
            logSocket = new LogSocket();
            log = new LogMaster(new Matching());
            logPlayer = new LogPlayer();
            log.Player = logPlayer;
            client = new Client(logSocket, log);
        }
        [Test]
        public void testProcessMessage()
        {

            client.Process(new Message(1));

            Assert.AreEqual("Match ", log.log);
            Assert.AreEqual("Send opcode:1 result:0", logSocket.log);

            client.Process(new Message(2));

            Assert.AreEqual("Match CancelMatch ", log.log);
            Assert.AreEqual("Send opcode:2 result:0", logSocket.log);

        }
        [Test]
        public void testPlayApi()
        {
            var message = new PlayMessage(1, 2);
            client.Process(message);

            Assert.AreEqual($"Play({message.x},{message.y}) ", logPlayer.log);
            Assert.AreEqual("Send opcode:3 result:29999", logSocket.log);
        }
        [Test]
        public void testPlayApiException()
        {
            var ex = Assert.Throws<InvalidCastException>(() =>
            {
                client.Process(new Message(3));
            });
        }
    }
}
