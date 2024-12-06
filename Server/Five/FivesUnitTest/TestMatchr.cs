using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMatchr
    {
        private LogPlayer player;
        private PlayerRepository mgr;
        private MatchServce servce;
        private LogSocket socket;
        [SetUp]
        public void SetUp()
        {
            player = LogPlayer.EmntyLog();
            var app = new App();
            mgr = app.playerRsp;
            servce = new MatchServce(app, new GameFactroy());
            socket = new LogSocket();
            mgr.Add(socket, player);
        }
        [Test]
        public void testMgrMatch()
        {
            var result = servce.Match(socket);
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual("Match ", player.log);
            Assert.AreEqual(1, player.RoomId);
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
            Assert.AreEqual("Match Reset CancelMatch ", player.log);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatchOnRealGameStart()
        {
            var player1 = new LogPlayer();
            var socket1 = new LogSocket();
            mgr.Add(socket1, player1);
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
