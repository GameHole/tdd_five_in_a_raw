using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMatchr
    {
        LogMatcher master;
        private ClientMgr mgr;
        private LogSocket socket;
        Matching matching;
        [SetUp]
        public void SetUp()
        {
            matching = new Matching();
            master = new LogMatcher();
            mgr = new ClientMgr(matching);
            mgr.rsp = new ClientRsp(new RequestRegister(mgr));
            socket = new LogSocket();
            mgr.matchers.TryAdd(socket, master);
        }
        [Test]
        public void testPlayerStart()
        {
            master.Player.Start(1);
            Assert.AreEqual("Start ", master.log);
        }
        [Test]
        public void testPlayerFinish()
        {
            master.Player.Finish();
            Assert.AreEqual("Finished ", master.log);
        }
        [Test]
        public void testMgrMatch()
        {
            var result = mgr.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual("Match ", master.log);
            Assert.AreEqual(1, master.GameId);
            result = mgr.Match(socket);
            Assert.AreEqual(ResultDefine.Matching, result);
            result = mgr.Cancel(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            result = mgr.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatch()
        {
            var result = mgr.Cancel(socket);
            Assert.AreEqual(ResultDefine.NotInMatching, result);
            result = mgr.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            result = mgr.Cancel(socket);
            Assert.AreEqual("Match CancelMatch ", master.log);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatchOnRealGameStart()
        {
            var match1 = new LogMatcher();
            var socket1 = new LogSocket();
            mgr.matchers.TryAdd(socket1, match1);
            mgr.Match(socket1);
            mgr.Match(socket);
            Assert.AreEqual("Match Start ", master.log);
            var result = mgr.Match(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
            result = mgr.Cancel(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
    }
}
