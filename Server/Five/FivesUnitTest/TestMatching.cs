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
        private TClient client;
        private LogPlayer player;
        private MatchServce servce;

        [SetUp]
        public void SetUp()
        {
            var app = new Domain(new GameFactroy(),new TIdGenrator());
            gameRsp = app.roomRsp;
            mgr = app.playerRsp;
            client = new TClient();
            player = LogPlayer.EmntyLog();
            mgr.Add(client.Id, player);
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
            mgr.Add(socket.Id, player);
            servce.Match(socket.Id);
            Assert.AreEqual("Match ", player.log);
            Assert.AreNotEqual(0, player.RoomId);
            Assert.AreEqual(1, gameRsp.GameCount);
            Assert.AreEqual(1, gameRsp.GetRoom(player.RoomId).PlayerCount);
        }
        [Test]
        public void testMatchTwoPlayer()
        {
            var players = new LogPlayer[2];
            var client = new TClient[2];
            for (int i = 0; i < players.Length; i++)
            {
                var p = LogPlayer.EmntyLog();
                players[i] = p;
                client[i] = new TClient();
                mgr.Add(client[i].Id, p);
            }
            for (int i = 0; i < players.Length; i++)
            {
                servce.Match(client[i].Id);
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
            var client = new TClient[4];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogPlayer();
                client[i] = new TClient();
                mgr.Add(client[i].Id, players[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                servce.Match(client[i].Id);
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
            servce.Match(client.Id);
            var game = gameRsp.GetRoom(1);
            gameRsp.Clear();
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, gameRsp.GameCount);
        }
        [Test]
        public void testCancel()
        {
            servce.Match(client.Id);
            int id = player.RoomId;
            Assert.AreEqual(ResultDefine.Success, servce.Cancel(client.Id)); 
            Assert.AreEqual(0, gameRsp.GetRoom(id).PlayerCount);
            Assert.AreEqual("Match Reset CancelMatch ", player.log);
        }
        [Test]
        public void testCancelOnGameStart()
        {
            LogPlayer[] player = new LogPlayer[2];
            var client = new TClient[2];
            for (int i = 0; i < player.Length; i++)
            {
                client[i] = new TClient();
                var p = LogPlayer.EmntyLog();
                player[i] = p;
                mgr.Add(client[i].Id, p);
                servce.Match(client[i].Id);
            }
            var room = gameRsp.GetRoom(player[0].RoomId);
            Assert.AreEqual(ResultDefine.GameStarted, servce.Cancel(client[0].Id));
            Assert.AreEqual(2, room.PlayerCount);
            Assert.AreEqual("Match Start ", player[0].log);
            (room.game as Game).Finish(1);
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(client[0].Id));
        }
        [Test]
        public void testCancelNotInGame()
        {
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(client.Id));
        }
        [Test]
        public void testMgrMatch()
        {
            var result = servce.Match(client.Id);
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual("Match ", player.log);
            Assert.AreEqual(1, player.RoomId);
            result = servce.Match(client.Id);
            Assert.AreEqual(ResultDefine.Matching, result);
            result = servce.Cancel(client.Id);
            Assert.AreEqual(ResultDefine.Success, result);
            result = servce.Match(client.Id);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatch()
        {
            var result = servce.Cancel(client.Id);
            Assert.AreEqual(ResultDefine.NotInMatching, result);
            result = servce.Match(client.Id);
            Assert.AreEqual(ResultDefine.Success, result);
            result = servce.Cancel(client.Id);
            Assert.AreEqual("Match Reset CancelMatch ", player.log);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatchOnRealGameStart()
        {
            var player1 = new LogPlayer();
            var socket1 = new LogSocket();
            mgr.Add(socket1.Id, player1);
            servce.Match(socket1.Id);
            servce.Match(client.Id);
            Assert.AreEqual("Match Start ", player.log);
            var result = servce.Match(client.Id);
            Assert.AreEqual(ResultDefine.GameStarted, result);
            result = servce.Cancel(client.Id);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
    }
}
