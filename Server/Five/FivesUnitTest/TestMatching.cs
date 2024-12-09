using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMatching
    {
        RoomRepository gameRsp;
        private PlayerRepository mgr;
        private LogSocket socket;
        private LogPlayer player;
        private MatchServce servce;

        [SetUp]
        public void SetUp()
        {
            var app = new Domain(new GameFactroy(),new TIdGenrator());
            gameRsp = app.roomRsp;
            mgr = app.playerRsp;
            socket = new LogSocket();
            player = LogPlayer.EmntyLog();
            mgr.Add(socket, player);
            servce = new MatchServce(app);
        }
        [Test]
        public void testMatching()
        {
            Assert.AreEqual(0,gameRsp.GameCount);
        }
        [Test]
        public void testMatchOnePlayer()
        {
            var player = LogPlayer.EmntyLog();
            var socket = new LogSocket();
            mgr.Add(socket, player);
            servce.Match(socket);
            Assert.AreEqual("Match ", player.log);
            Assert.AreNotEqual(0, player.RoomId);
            Assert.AreEqual(1, gameRsp.GameCount);
            Assert.AreEqual(1, gameRsp.GetRoom(player.RoomId).PlayerCount);
        }
        [Test]
        public void testMatchTwoPlayer()
        {
            var players = new LogPlayer[2];
            var sockets = new LogSocket[2];
            for (int i = 0; i < players.Length; i++)
            {
                var p = LogPlayer.EmntyLog();
                players[i] = p;
                sockets[i] = new LogSocket();
                mgr.Add(sockets[i], p);
            }
            for (int i = 0; i < players.Length; i++)
            {
                servce.Match(sockets[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                Assert.AreEqual("Match Start ", players[i].log);
            }
        }
        [Test]
        public void testMatchMutltyPlayer()
        {
            var players = new LogPlayer[4];
            var sockets = new LogSocket[4];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogPlayer();
                sockets[i] = new LogSocket();
                mgr.Add(sockets[i], players[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                servce.Match(sockets[i]);
            }
            Assert.AreNotEqual(players[0].RoomId, players[2].RoomId);
        }
        [Test]
        public void testGetGame()
        {
            Assert.AreEqual(null, gameRsp.GetRoom(0));
        }
        [Test]
        public void testClear()
        {
            servce.Match(socket);
            var game = gameRsp.GetRoom(1);
            gameRsp.Clear();
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, gameRsp.GameCount);
        }
        [Test]
        public void testCancel()
        {
            servce.Match(socket);
            int id = player.RoomId;
            Assert.AreEqual(ResultDefine.Success, servce.Cancel(socket)); 
            Assert.AreEqual(0, gameRsp.GetRoom(id).PlayerCount);
            Assert.AreEqual("Match Reset CancelMatch ", player.log);
        }
        [Test]
        public void testCancelOnGameStart()
        {
            LogPlayer[] player = new LogPlayer[2];
            var sockets = new LogSocket[2];
            for (int i = 0; i < player.Length; i++)
            {
                sockets[i] = new LogSocket();
                var p = LogPlayer.EmntyLog();
                player[i] = p;
                mgr.Add(sockets[i], p);
                servce.Match(sockets[i]);
            }
            var room = gameRsp.GetRoom(player[0].RoomId);
            Assert.AreEqual(ResultDefine.GameStarted, servce.Cancel(sockets[0]));
            Assert.AreEqual(2, room.PlayerCount);
            Assert.AreEqual("Match Start ", player[0].log);
            (room.game as Game).Finish(1);
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(sockets[0]));
        }
        [Test]
        public void testCancelNotInGame()
        {
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(socket));
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
