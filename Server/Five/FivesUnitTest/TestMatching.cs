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

        [SetUp]
        public void SetUp()
        {
            gameRsp = new GameMgr();
            mgr = new MatcherMgr(gameRsp);
        }
        [Test]
        public void testMatching()
        {
            Assert.AreEqual(0,gameRsp.GameCount);
        }
        [Test]
        public void testMatchOnePlayer()
        {
            var master = new LogMatcher();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, master);
            mgr.Match(socket);
            Assert.AreEqual("Match ", master.log);
            Assert.AreNotEqual(0, master.GameId);
            Assert.AreEqual(1, gameRsp.GameCount);
            Assert.AreEqual(1, gameRsp.GetGame(master.GameId).PlayerCount);
        }
        [Test]
        public void testMatchTwoPlayer()
        {
            var players = new LogMatcher[2];
            var sockets = new LogSocket[2];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogMatcher();
                sockets[i] = new LogSocket();
                mgr.matchers.TryAdd(sockets[i], players[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                mgr.Match(sockets[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                Assert.AreEqual("Match Start ", players[i].log);
            }
        }
        [Test]
        public void testMatchMutltyPlayer()
        {
            var players = new LogMatcher[4];
            var sockets = new LogSocket[4];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogMatcher();
                sockets[i] = new LogSocket();
                mgr.matchers.TryAdd(sockets[i], players[i]);
            }
            for (int i = 0; i < players.Length; i++)
            {
                mgr.Match(sockets[i]);
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
            var master = new LogMatcher();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, master);
            mgr.Match(socket);
            var game = gameRsp.GetGame(1);
            gameRsp.Clear();
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, gameRsp.GameCount);
        }
        [Test]
        public void testCancel()
        {
            var player = new LogMatcher();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, player);
            mgr.Match(socket);
            int id = player.GameId;
            Assert.AreEqual(ResultDefine.Success, mgr.Cancel(socket)); 
            Assert.AreEqual(0, gameRsp.GetGame(id).PlayerCount);
            Assert.AreEqual("Match CancelMatch ", player.log);
        }
        [Test]
        public void testCancelOnGameStart()
        {
            LogMatcher[] player = new LogMatcher[2];
            var sockets = new LogSocket[2];
            for (int i = 0; i < player.Length; i++)
            {
                sockets[i] = new LogSocket();
                player[i] = new LogMatcher();
                mgr.matchers.TryAdd(sockets[i], player[i]);
                mgr.Match(sockets[i]);
            }
            var game = gameRsp.GetGame(player[0].GameId);
            Assert.AreEqual(ResultDefine.GameStarted, mgr.Cancel(sockets[0]));
            Assert.AreEqual(2, game.PlayerCount);
            Assert.AreEqual("Match Start ", player[0].log);
            game.Finish(1);
            Assert.AreEqual(ResultDefine.NotInMatching, mgr.Cancel(sockets[0]));
        }
        [Test]
        public void testCancelNotInGame()
        {
            var player = new LogMatcher();
            var socket = new LogSocket();
            mgr.matchers.TryAdd(socket, player);
            Assert.AreEqual(ResultDefine.NotInMatching, mgr.Cancel(socket));
        }
    }
}
