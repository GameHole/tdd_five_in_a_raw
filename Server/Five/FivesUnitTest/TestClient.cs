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
            client = new LogClient(logSocket);
            new RequestRegister(logSocket, logMatcher).Regist(client);
        }
        [Test]
        public void testRunProcess()
        {
            var processer = new LogProcesser();
            client.Processers.Add(processer.MessageCode,processer);
            client.Process(new Message(processer.MessageCode));
            Assert.AreEqual("Process", processer.log);
        }
        [Test]
        public void testRegistedProcess()
        {
            Assert.IsTrue(client.Processers.Contains(MessageCode.RequestMatch));
            Assert.IsTrue(client.Processers.Contains(MessageCode.RequestCancelMatch));
            Assert.IsTrue(client.Processers.Contains(MessageCode.RequestPlay));
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
            var message = new PlayRequest(1, 2);
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
        public void testRecvForProcess()
        {
            logSocket.onRecv(new Message(1));
            Assert.AreEqual("Process", client.log);
        }
    }
}
