using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMatching
    {
        Matching matching;
        [SetUp]
        public void SetUp()
        {
            matching = new Matching();
        }
        [Test]
        public void testMatching()
        {
            Assert.AreEqual(0,matching.GameCount);
        }
        [Test]
        public void testMatchOnePlayer()
        {
            var master = new LogMatcher();
            matching.Match(master);
            Assert.AreEqual("Match ", master.log);
            Assert.AreNotEqual(0, master.GameId);
            Assert.AreEqual(1, matching.GameCount);
            Assert.AreEqual(1, matching.GetGame(master.GameId).PlayerCount);
        }
        [Test]
        public void testMatchTwoPlayer()
        {
            var players = new LogMatcher[2];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogMatcher();
            }
            for (int i = 0; i < players.Length; i++)
            {
                matching.Match(players[i]);
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
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogMatcher();
            }
            for (int i = 0; i < players.Length; i++)
            {
                matching.Match(players[i]);
            }
            Assert.AreNotEqual(players[0].GameId, players[2].GameId);
        }
        [Test]
        public void testGetGame()
        {
            Assert.AreEqual(null, matching.GetGame(0));
        }
        [Test]
        public void testClear()
        {
            matching.Match(new LogMatcher());
            var game = matching.GetGame(1);
            matching.Clear();
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, matching.GameCount);
        }
        [Test]
        public void testCancel()
        {
            var player = new LogMatcher();
            matching.Match(player);
            int id = player.GameId;
            Assert.AreEqual(ResultDefine.Success, matching.Cancel(player)); 
            Assert.AreEqual(0, matching.GetGame(id).PlayerCount);
            Assert.AreEqual("Match CancelMatch ", player.log);
        }
        [Test]
        public void testCancelOnGameStart()
        {
            LogMatcher[] player = new LogMatcher[2];
            for (int i = 0; i < player.Length; i++)
            {
                player[i] = new LogMatcher();
                matching.Match(player[i]);
            }
            var game = matching.GetGame(player[0].GameId);
            Assert.AreEqual(ResultDefine.GameStarted, matching.Cancel(player[0]));
            Assert.AreEqual(2, game.PlayerCount);
            Assert.AreEqual("Match Start ", player[0].log);
            game.Finish(1);
            Assert.AreEqual(ResultDefine.NotInMatching, matching.Cancel(player[0]));
        }
        [Test]
        public void testCancelNotInGame()
        {
            Assert.AreEqual(ResultDefine.NotInMatching, matching.Cancel(new LogMatcher()));
        }
    }
}
