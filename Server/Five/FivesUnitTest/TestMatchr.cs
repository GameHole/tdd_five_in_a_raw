﻿using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMatchr
    {
        private LogPlayer player;
        private MatcherMgr mgr;
        private MatchServce servce;
        private LogSocket socket;
        GameMgr matching;
        [SetUp]
        public void SetUp()
        {
            matching = new GameMgr();
            player = new LogPlayer();
            mgr = new MatcherMgr();
            servce = new MatchServce(mgr,matching);
            socket = new LogSocket();
            mgr.matchers.TryAdd(socket, player);
        }
        [Test]
        public void testMgrMatch()
        {
            var result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual("Match ", player.log);
            Assert.AreEqual(1, player.GameId);
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
            Assert.AreEqual("Match CancelMatch ", player.log);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatchOnRealGameStart()
        {
            var player1 = new LogPlayer();
            var socket1 = new LogSocket();
            mgr.matchers.TryAdd(socket1, player1);
            servce.Match(socket1);
            servce.Match(socket);
            Assert.AreEqual("Match Start ", player.log);
            var result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
            result = servce.Cancel(socket);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
    }
}
