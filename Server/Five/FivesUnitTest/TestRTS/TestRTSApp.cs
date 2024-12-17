using Five;
using Five.RTS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest.RTS
{
    internal class TestRTSApp
    {
        private LogSocket[] clients;
        private RTGGameFactroy gameFact;
        private Domain domain;
        private ServerProcesser app;

        [SetUp]
        public void set()
        {
            gameFact = new RTGGameFactroy();
            domain = new Domain(gameFact, new IdGenrator());
            clients = new LogSocket[2];
            app = new RTSProcessFactroy(domain).Factroy();
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i] = new LogSocket();
                app.OnConnect(clients[i]);
            }
        }
        [Test]
        public void testLogin()
        {
            var assert = new AppAssertion(app);
            Assert.IsInstanceOf<ConnectProcesser>(app.connect);
            Assert.IsInstanceOf<ServerStopProcesser>(app.serverStop);
            Assert.IsInstanceOf<OpCodeErrorResponseProcesser>(app.defaultProcesser);
            assert.AssertProcesserIs<MatchRequestProcesser>(MessageCode.RequestMatch);
            assert.AssertProcesserIs<CancelRequestProcesser>(MessageCode.RequestCancelMatch);
            assert.AssertProcesserIs<PlayRequestProcesser>(MessageCode.RequestPlay);
            Assert.AreEqual(2, domain.playerRsp.Count);
        }

      

        [Test]
        public void testStart()
        {
            
        }
    }
}
