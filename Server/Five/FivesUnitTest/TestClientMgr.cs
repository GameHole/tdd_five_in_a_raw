using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesUnitTest
{
    class TestClientMgr
    {
        private LogRequestRegister log;
        private ClientRsp rsp;
        ClientMgr mgr;
        [SetUp]
        public void SetUp()
        {
            mgr = new ClientMgr(new Matching());
            log = new LogRequestRegister(mgr);
            rsp = new ClientRsp(log);
        }
        [Test]
        public void testClientMgr()
        {
            Assert.AreEqual(0, mgr.matchers.Count);
        }
        [Test]
        public void testonAcceptMgr()
        {
            var socket = new LogSocket();
            mgr.Invoke(socket);

            Assert.AreEqual(1, mgr.matchers.Count);
            var matcher = mgr.matchers[socket];
            Assert.AreEqual(typeof(NetNotifier), matcher.Player.notifier.GetType());

            var logP = new LogPlayer();
            matcher.Player = logP;
            socket.onClose.Invoke();
            Assert.AreEqual("OutLine ", logP.log);
            Assert.AreEqual(0, mgr.matchers.Count);
        }
        [Test]
        public void testClientRsp()
        {
            Assert.AreEqual(typeof(OpCodeErrorResponseProcesser),rsp.processer.defaultProcesser.GetType());
            Assert.IsTrue(rsp.processer.Processers.Contains(MessageCode.RequestMatch));
            Assert.IsTrue(rsp.processer.Processers.Contains(MessageCode.RequestCancelMatch));
            Assert.IsTrue(rsp.processer.Processers.Contains(MessageCode.RequestPlay));
            Assert.IsTrue(rsp.processer.Processers.Contains(-1));
        }
        [Test]
        public void testonAccept()
        {
            var socket = new LogSocket();
            rsp.Invoke(socket);

            Assert.IsNull(log.test.Client);
            Assert.AreSame(mgr, log.test.Mgr);
           
            socket.onRecv(new Message { opcode=-1});
            Assert.AreSame(socket, log.test.msgSock);

            Assert.IsNull( socket.onClose);
        }
        [Test]
        public void testSocketClose()
        {
            var scket = new LogSocket();
            mgr.Invoke(scket);
            scket.Close();
            Assert.AreEqual(0, mgr.matchers.Count);
        }
    }
}
