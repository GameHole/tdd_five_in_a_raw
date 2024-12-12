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
            var msg = new Message(processer.OpCode);
            msgProcesser.Process(logSocket, msg);
            Assert.AreEqual("Process", processer.log);
            Assert.AreEqual(logSocket, processer.client);
            Assert.AreEqual(msg, processer.msg);
        }
        [Test]
        public void testProcessMessage()
        {
            var mgr = new Domain(new GameFactroy(), new IdGenrator());
            var svc = new MatchServce(mgr);
            LogPlayer logPlayer = LogPlayer.EmntyLog();
            mgr.playerRsp.Add(logSocket.Id, logPlayer);

            var matchProcesser = new MatchRequestProcesser();
            matchProcesser.Init(svc);
            var cnacelProcesser = new CancelRequestProcesser();
            cnacelProcesser.Init(svc);
            var playProcesser = new PlayRequestProcesser();
            playProcesser.Init(svc);

            var opErrProcesser = new OpCodeErrorResponseProcesser();

            matchProcesser.Process(logSocket, new Message(MessageCode.RequestMatch));
            Assert.AreEqual("Match ", logPlayer.log);
            Assert.AreEqual("Send opcode:2 result:0", logSocket.log);

            cnacelProcesser.Process(logSocket, new Message(MessageCode.RequestCancelMatch));

            Assert.AreEqual("Match Reset CancelMatch ", logPlayer.log);
            Assert.AreEqual("Send opcode:4 result:0", logSocket.log);

            var message = new PlayRequest { x = 1, y = 2 };
            playProcesser.Process(logSocket, message);

            Assert.AreEqual($"Match Reset CancelMatch Play({message.x},{message.y}) ", logPlayer.log);
            Assert.AreEqual("Send opcode:6 result:29999", logSocket.log);

            opErrProcesser.Process(logSocket, new Message(200));
            Assert.AreEqual("Send opcode:-10000 unknown opcode:200", logSocket.log);
        }
        [Test]
        public void testLoginSvc()
        {
            var mgr = new Domain(new GameFactroy(), new TIdGenrator { id=2, inviled=1});
            var svc = new MatchServce(mgr);
            var loginSvc = new ConnectProcesser();
            loginSvc.Init(svc);
            loginSvc.Process(logSocket, default);
            Assert.AreEqual(2, logSocket.Id);
            var player = mgr.playerRsp.FindPlayer(logSocket.Id);
            Assert.AreEqual(player.Id, logSocket.Id);
            logSocket.onClose.Invoke();
            Assert.AreEqual(typeof(NoneNotifier), player.notifier.GetType());
            Assert.AreEqual(0, mgr.playerRsp.Count);
            Assert.AreEqual(1, logSocket.Id);
        }

        [Test]
        public void testCastException()
        {
            var play = new PlayRequestProcesser();
            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                play.Process(logSocket,new Message(MessageCode.RequestPlay));
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
            msgProcesser.Process(logSocket,new Message(999));
            Assert.AreEqual("Process", logProcesser.log);
        }
    }
}
