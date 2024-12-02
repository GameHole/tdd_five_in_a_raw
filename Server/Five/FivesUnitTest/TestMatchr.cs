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
            master = new LogMatcher(matching);
            mgr = new ClientMgr(matching);
            socket = new LogSocket();
            mgr.matchers.TryAdd(socket, master);
        }
        [Test]
        public void testMatch()
        {
            var result = master.Match();
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual("Match ", master.log);
            Assert.AreEqual(1, master.GameId);
            result = master.Match();
            Assert.AreEqual(ResultDefine.Matching, result);
            result = master.Cancel();
            Assert.AreEqual(ResultDefine.Success, result);
            result = master.Match();
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testCancelMatch()
        {
            var result = master.Cancel();
            Assert.AreEqual(ResultDefine.NotInMatching, result);
            result = master.Match();
            Assert.AreEqual(ResultDefine.Success, result);
            result = master.Cancel();
            Assert.AreEqual("Match CancelMatch ", master.log);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMatchOnGameStart()
        {
            master.Started();
            var result = master.Match();
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
        [Test]
        public void testCancelMatchOnGameStart()
        {
            master.Started();
            var result = master.Cancel();
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
        [Test]
        public void testCancelMatchOnRealGameStart()
        {
            var match1 = new LogMatcher(matching);
            match1.Match();
            master.Match();
            Assert.AreEqual("Match Start ", master.log);
            var result = master.Cancel();
            Assert.AreEqual(ResultDefine.GameStarted, result);
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
        public void testMgrMatchOnGameStart()
        {
            master.Started();
            var result = mgr.Match(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
        [Test]
        public void testMgrCancelMatchOnGameStart()
        {
            master.Started();
            var result = mgr.Cancel(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
        [Test]
        public void testMgrCancelMatchOnRealGameStart()
        {
            var match1 = new LogMatcher(matching);
            var socket1 = new LogSocket();
            mgr.matchers.TryAdd(socket1, match1);
            mgr.Match(socket1);
            mgr.Match(socket);
            Assert.AreEqual("Match Start ", master.log);
            var result = mgr.Cancel(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
    }
}
