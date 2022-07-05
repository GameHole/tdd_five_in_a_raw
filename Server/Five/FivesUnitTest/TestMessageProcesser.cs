using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMessageProcesser
    {
        LogSocket logSocket;
        LogMatcher logMatcher;
        LogPlayer logPlayer;
        LogClient client;
        LogProcesser logProcesser;
        [SetUp]
        public void SetUp()
        {
            logProcesser = new LogProcesser();
            logSocket = new LogSocket();
            logMatcher = new LogMatcher(new Matching());
            logPlayer = new LogPlayer();
            logMatcher.Player = logPlayer;
            client = new LogClient(logSocket, logProcesser);
            new RequestRegister(logSocket, logMatcher).Regist(client);
        }
        [Test]
        public void testRunProcess()
        {
            var processer = new LogProcesser();
            client.Processers.Add(processer.OpCode,processer);
            client.Process(new Message(processer.OpCode));
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
            var matchProcesser = new MatchRequestProcesser();
            matchProcesser.Init(logSocket, logMatcher);
            var cnacelProcesser = new CancelRequestProcesser();
            cnacelProcesser.Init(logSocket, logMatcher);
            var playProcesser = new PlayRequestProcesser();
            playProcesser.Init(logSocket, logMatcher);

            var opErrProcesser = new OpCodeErrorResponseProcesser(logSocket);

            matchProcesser.Process(new Message(MessageCode.RequestMatch));
            Assert.AreEqual("Match ", logMatcher.log);
            Assert.AreEqual("Send opcode:2 result:0 id:0", logSocket.log);

            cnacelProcesser.Process(new Message(MessageCode.RequestCancelMatch));

            Assert.AreEqual("Match CancelMatch ", logMatcher.log);
            Assert.AreEqual("Send opcode:4 result:0", logSocket.log);

            var message = new PlayRequest { x = 1, y = 2 };
            playProcesser.Process(message);

            Assert.AreEqual($"Play({message.x},{message.y}) ", logPlayer.log);
            Assert.AreEqual("Send opcode:6 result:29999", logSocket.log);

            opErrProcesser.Process(new Message(200));
            Assert.AreEqual("Send opcode:-10000 unknown opcode:200", logSocket.log);
        }
        [Test]
        public void testCastException()
        {
            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                client.Process(new Message(MessageCode.RequestPlay));
            });
        }
        [Test]
        public void testProcessUnkonwnMessage()
        {
            client.Process(new Message(999));
            Assert.AreEqual("Process", logProcesser.log);
        }
        [Test]
        public void testRecvForProcess()
        {
            logSocket.onRecv(new Message(1));
            Assert.AreEqual("Process", client.log);
        }
    }
}
