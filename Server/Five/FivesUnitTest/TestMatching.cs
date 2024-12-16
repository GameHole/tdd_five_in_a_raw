using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMatching
    {
        RoomRepository roomRsp;
        private PlayerRepository mgr;
        private LogPlayer player;
        private MatchServce servce;
        int clientId;
        [SetUp]
        public void SetUp()
        {
            clientId = 10000;
            var app = new Domain(new GameFactroy(),new TIdGenrator());
            roomRsp = app.roomRsp;
            mgr = app.playerRsp;
            player = LogPlayer.EmntyLog(clientId);
            mgr.Add(player);
            servce = app.matchServce;
        }
        [Test]
        public void testMatching()
        {
            Assert.AreEqual(0,roomRsp.Count);
        }
        [Test]
        public void testMatchOnePlayer()
        {
            servce.Match(clientId);
            Assert.AreNotEqual(0, player.RoomId);
            Assert.AreEqual(1, roomRsp.Count);
            Assert.AreEqual(1, roomRsp.GetRoom(player.RoomId).PlayerCount);
            Assert.IsTrue(roomRsp.GetRoom(player.RoomId).ContainPlayer(player));
        }
        [Test]
        public void testMatchTwoPlayer()
        {
            var players = new Player[2];
            for (int i = 0; i < players.Length; i++)
            {
                var p = new Player(i);
                players[i] = p;
                mgr.Add(p);
            }
            for (int i = 0; i < players.Length; i++)
            {
                servce.Match(i);
            }
            var room = roomRsp.GetRoom(1);
            for (int i = 0; i < players.Length; i++)
            {
                Assert.AreEqual(1, players[i].RoomId);
                Assert.IsTrue(room.ContainPlayer(players[i]));
                Assert.IsTrue(room.IsRunning);
            }
        }
        [Test]
        public void testMatchMutltyPlayer()
        {
            var players = new Player[4];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(i);
                mgr.Add(players[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                servce.Match(i);
            }
            Assert.AreNotEqual(players[0].RoomId, players[2].RoomId);
        }
        [Test]
        public void testGetGame()
        {
            Assert.AreEqual(null, roomRsp.GetRoom(0));
        }
        [Test]
        public void testClear()
        {
            servce.Match(clientId);
            var game = roomRsp.GetRoom(1);
            roomRsp.Clear();
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, roomRsp.Count);
        }
        [Test]
        public void testCancel()
        {
            servce.Match(clientId);
            var room = roomRsp.GetRoom(player.RoomId);
            Assert.AreEqual(ResultDefine.Success, servce.Cancel(clientId)); 
            Assert.AreEqual(0, room.PlayerCount);
            Assert.IsFalse(room.ContainPlayer(player));
            Assert.AreEqual(0, player.RoomId);
        }
        [Test]
        public void testCancelOnGameStart()
        {
            LogPlayer[] player = new LogPlayer[2];
            for (int i = 0; i < player.Length; i++)
            {
                var p = LogPlayer.EmntyLog(i);
                player[i] = p;
                mgr.Add(p);
                servce.Match(i);
            }
            var room = roomRsp.GetRoom(player[0].RoomId);
            Assert.AreEqual(ResultDefine.GameStarted, servce.Cancel(0));
            Assert.AreEqual(2, room.PlayerCount);
            Assert.IsTrue(room.IsRunning);
            (room.game as Game).Finish(1);
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(0));
        }
        [Test]
        public void testCancelNotInGame()
        {
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(clientId));
        }
        [Test]
        public void testMgrMatch()
        {
            var result = servce.Match(clientId);
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual(1, player.RoomId);
            result = servce.Match(clientId);
            Assert.AreEqual(ResultDefine.Matching, result);
            result = servce.Cancel(clientId);
            Assert.AreEqual(ResultDefine.Success, result);
            result = servce.Match(clientId);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatch()
        {
            var result = servce.Cancel(clientId);
            Assert.AreEqual(ResultDefine.NotInMatching, result);
            result = servce.Match(clientId);
            Assert.AreEqual(1, player.RoomId);
            Assert.AreEqual(ResultDefine.Success, result);
            result = servce.Cancel(clientId);
            Assert.AreEqual(0, player.RoomId);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMgrCancelMatchOnRealGameStart()
        {
            var player1 = new Player(10001);
            mgr.Add(player1);
            servce.Match(10001);
            servce.Match(clientId);
            var result = servce.Match(clientId);
            Assert.AreEqual(ResultDefine.GameStarted, result);
            result = servce.Cancel(clientId);
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
    }
}
