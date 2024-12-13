using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMessageProcesser
    {
        LogSocket logClient;
        MessageProcesser msgProcesser;
        LogProcesser logProcesser;
        [SetUp]
        public void SetUp()
        {
            logProcesser = new LogProcesser();
            logClient = new LogSocket();
            msgProcesser = new MessageProcesser(logProcesser);
        }
        [Test]
        public void testRunProcess()
        {
            var processer = new LogProcesser();
            msgProcesser.Processers.Add(10000, processer);
            var msg = new Message(10000);
            msgProcesser.Process(logClient, msg);
            Assert.AreEqual("Process", processer.log);
            Assert.AreEqual(logClient, processer.client);
            Assert.AreEqual(msg, processer.msg);
        }
        [Test]
        public void testProcessMessage()
        {
            var mgr = new Domain(new GameFactroy(), new IdGenrator());
            var svc = new MatchServce(mgr);
            LogPlayer logPlayer = LogPlayer.EmntyLog(0);
            mgr.playerRsp.Add(logPlayer);

            var matchProcesser = new MatchRequestProcesser();
            matchProcesser.Init(svc);
            var cnacelProcesser = new CancelRequestProcesser();
            cnacelProcesser.Init(svc);
            var playProcesser = new PlayRequestProcesser();
            playProcesser.Init(svc);

            var opErrProcesser = new OpCodeErrorResponseProcesser();

            matchProcesser.Process(logClient, new Message(MessageCode.RequestMatch));
            Assert.AreEqual("Match ", logPlayer.log);
            Assert.AreEqual("Send opcode:2 result:0", logClient.log);

            cnacelProcesser.Process(logClient, new Message(MessageCode.RequestCancelMatch));

            Assert.AreEqual("Match Reset CancelMatch ", logPlayer.log);
            Assert.AreEqual("Send opcode:4 result:0", logClient.log);

            var message = new PlayRequest { x = 1, y = 2 };
            playProcesser.Process(logClient, message);

            Assert.AreEqual($"Match Reset CancelMatch Play({message.x},{message.y}) ", logPlayer.log);
            Assert.AreEqual("Send opcode:6 result:29999", logClient.log);

            opErrProcesser.Process(logClient, new Message(200));
            Assert.AreEqual("Send opcode:-10000 unknown opcode:200", logClient.log);
        }
        [Test]
        public void testLoginSvc()
        {
            var mgr = new Domain(new GameFactroy(), new TIdGenrator { id=2, inviled=1});
            var svc = new MatchServce(mgr);
            var loginSvc = new ConnectProcesser();
            loginSvc.Init(svc);
            loginSvc.Process(logClient, default);
            Assert.AreEqual(2, logClient.Id);
            var player = mgr.playerRsp.FindPlayer(logClient.Id);
            Assert.AreSame(logClient, player.notifier);
            Assert.AreEqual(player.Id, logClient.Id);
            logClient.onClose.Invoke();
            Assert.AreEqual(typeof(NoneNotifier), player.notifier.GetType());
            Assert.AreEqual(0, mgr.playerRsp.Count);
            Assert.AreEqual(2, logClient.Id);
        }

        [Test]
        public void testNoCastException()
        {
            var play = new PlayRequestProcesser();
            var mgr = new Domain(new GameFactroy(), new TIdGenrator { id = 2, inviled = 1 });
            var svc = new MatchServce(mgr);
            play.Init(svc);
            mgr.playerRsp.Add(new Player());
            Assert.DoesNotThrow(() =>
            {
                play.Process(logClient,new Message(MessageCode.RequestPlay));
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
            msgProcesser.Process(logClient,new Message(999));
            Assert.AreEqual("Process", logProcesser.log);
        }
    }
}
