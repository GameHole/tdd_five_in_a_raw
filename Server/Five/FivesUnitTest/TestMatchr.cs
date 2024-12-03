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
        private MatcherMgr mgr;
        private MatchServce servce;
        private LogSocket socket;
        GameMgr matching;
        [SetUp]
        public void SetUp()
        {
            matching = new GameMgr();
            master = new LogMatcher();
            mgr = new MatcherMgr();
            servce = new MatchServce(mgr,matching);
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
            var result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual("Match ", master.log);
            Assert.AreEqual(1, master.GameId);
            result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.Matching, result);
            result = servce.Cancel(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatch()
        {
            var result = servce.Cancel(socket);
            Assert.AreEqual(ResultDefine.NotInMatching, result);
            result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            result = servce.Cancel(socket);
            Assert.AreEqual("Match CancelMatch ", master.log);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatchOnRealGameStart()
        {
            var match1 = new LogMatcher();
            var socket1 = new LogSocket();
            mgr.matchers.TryAdd(socket1, match1);
            servce.Match(socket1);
            servce.Match(socket);
            Assert.AreEqual("Match Start ", master.log);
            var result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
            result = servce.Cancel(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
    }
}
