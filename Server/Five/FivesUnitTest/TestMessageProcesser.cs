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
        MessageProcesser msgProcesser;
        LogProcesser logProcesser;
        [SetUp]
        public void SetUp()
        {
            logProcesser = new LogProcesser();
            logSocket = new LogSocket();
            msgProcesser = new MessageProcesser(logProcesser);
        }
        [Test]
        public void testRunProcess()
        {
            var processer = new LogProcesser();
            msgProcesser.Processers.Add(processer.OpCode,processer);
            msgProcesser.Process(new Message(processer.OpCode));
            Assert.AreEqual("Process", processer.log);
        }
        [Test]
        public void testProcessMessage()
        {
            var mgr = new ClientMgr(new Matching());

            LogMatcher logMatcher = new LogMatcher(new Matching());
            LogPlayer logPlayer = new LogPlayer();
            logMatcher.Player = logPlayer;
            mgr.matchers.TryAdd(logSocket, logMatcher);

            var matchProcesser = new MatchRequestProcesser();
            matchProcesser.Init(logSocket, mgr);
            var cnacelProcesser = new CancelRequestProcesser();
            cnacelProcesser.Init(logSocket, mgr);
            var playProcesser = new PlayRequestProcesser();
            playProcesser.Init(logSocket, mgr);

            var opErrProcesser = new OpCodeErrorResponseProcesser(logSocket);

            matchProcesser.Process(new Message(MessageCode.RequestMatch));
            Assert.AreEqual("Match ", logMatcher.log);
            Assert.AreEqual("Send opcode:2 result:0", logSocket.log);

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
            var play = new PlayRequestProcesser();
            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                play.Process(new Message(MessageCode.RequestPlay));
            });
        }
        [Test]
        public void testCast()
        {
            var play = new PlayMessage();
            Assert.Null(play as PlayRequest);
        }
        [Test]
        public void testProcessUnkonwnMessage()
        {
            msgProcesser.Process(new Message(999));
            Assert.AreEqual("Process", logProcesser.log);
        }
    }
}
