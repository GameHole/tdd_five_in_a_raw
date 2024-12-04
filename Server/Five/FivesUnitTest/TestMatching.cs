using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMatching
    {
        GameMgr gameRsp;
        private MatcherMgr mgr;
        private MatchServce servce;

        [SetUp]
        public void SetUp()
        {
            gameRsp = new GameMgr();
            mgr = new MatcherMgr();
            servce = new MatchServce(mgr,gameRsp);
        }
        [Test]
        public void testMatching()
        {
            Assert.AreEqual(0,gameRsp.GameCount);
        }
        [Test]
        public void testMatchOnePlayer()
        {
            var player = new LogPlayer();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, player);
            servce.Match(socket);
            Assert.AreEqual("Match ", player.log);
            Assert.AreNotEqual(0, player.GameId);
            Assert.AreEqual(1, gameRsp.GameCount);
            Assert.AreEqual(1, gameRsp.GetGame(player.GameId).PlayerCount);
        }
        [Test]
        public void testMatchTwoPlayer()
        {
            var players = new LogPlayer[2];
            var sockets = new LogSocket[2];
            for (int i = 0; i < players.Length; i++)
            {
                var p = new LogPlayer();
                players[i] = p;
                sockets[i] = new LogSocket();
                mgr.matchers.TryAdd(sockets[i], p);
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
                mgr.matchers.TryAdd(sockets[i], players[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                servce.Match(sockets[i]);
            }
            Assert.AreNotEqual(players[0].GameId, players[2].GameId);
        }
        [Test]
        public void testGetGame()
        {
            Assert.AreEqual(null, gameRsp.GetGame(0));
        }
        [Test]
        public void testClear()
        {
            var master = new LogPlayer();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, master);
            servce.Match(socket);
            var game = gameRsp.GetGame(1);
            gameRsp.Clear();
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, gameRsp.GameCount);
        }
        [Test]
        public void testCancel()
        {
            var player = new LogPlayer();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, player);
            servce.Match(socket);
            int id = player.GameId;
            Assert.AreEqual(ResultDefine.Success, servce.Cancel(socket)); 
            Assert.AreEqual(0, gameRsp.GetGame(id).PlayerCount);
            Assert.AreEqual("Match CancelMatch ", player.log);
        }
        [Test]
        public void testCancelOnGameStart()
        {
            LogPlayer[] player = new LogPlayer[2];
            var sockets = new LogSocket[2];
            for (int i = 0; i < player.Length; i++)
            {
                sockets[i] = new LogSocket();
                var p = new LogPlayer();
                player[i] = p;
                mgr.matchers.TryAdd(sockets[i], p);
                servce.Match(sockets[i]);
            }
            var game = gameRsp.GetGame(player[0].GameId);
            Assert.AreEqual(ResultDefine.GameStarted, servce.Cancel(sockets[0]));
            Assert.AreEqual(2, game.PlayerCount);
            Assert.AreEqual("Match Start ", player[0].log);
            game.Finish(1);
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(sockets[0]));
        }
        [Test]
        public void testCancelNotInGame()
        {
            var player = new LogPlayer();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, player);
            Assert.AreEqual(ResultDefine.NotInMatching, servce.Cancel(socket));
        }
    }
}
