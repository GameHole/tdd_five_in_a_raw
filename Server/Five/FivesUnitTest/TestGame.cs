using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestGame
    {
        Game game;
        [SetUp]
        public void SetUp()
        {
            game = new Game();
            game.Id = 10;
        }
        [Test]
        public void testGame()
        {
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, game.turn.index);
            Assert.AreEqual(30, game.timer.time);
            Assert.AreEqual(15, game.chessboard.width);
            Assert.AreEqual(15, game.chessboard.height);
        }
        [Test]
        public void testGameStart()
        {
            var players = new LogPlayer[2];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new LogPlayer();
                game.Join(players[i]);
            }
            game.turn.index = 1;
            Assert.IsFalse(game.IsRunning);
            game.Start();
            Assert.IsTrue(game.IsRunning);
            for (int i = 0; i < players.Length; i++)
            {
                var item = players[i];
                Assert.AreEqual("Start ", item.log);
                Assert.AreEqual(i + 1, item.chess);
                item.Play(0, i);
                Assert.AreEqual(item.chess, game.chessboard.GetValue(0, i));
            }
        }
        [Test]
        public void testGetPlayer()
        {
            var player = game.GetPlayer(0);
            Assert.AreEqual(null, player);
        }
        [Test]
        public void testFull()
        {
            game.Join(new Player());
            Assert.IsFalse(game.isFull());
            var p = new Player();
            game.Join(p);
            Assert.IsTrue(game.isFull());
            game.Remove(p);
            Assert.IsFalse(game.isFull());
        }
        [Test]
        public void testJoinPlayer()
        {
            var player0 = new Player();
            Assert.AreEqual(-1, player0.PlayerId);
            Assert.IsTrue(game.Join(player0));
            Assert.IsTrue(game.ContainPlayer(player0));
            Assert.AreEqual(0, player0.PlayerId);
            Assert.AreEqual(game.Id, player0.GameId);
            Assert.AreEqual(1, game.PlayerCount);
            Assert.AreEqual(player0, game.GetPlayer(player0.PlayerId));
        }
        [Test]
        public void testJoinMaxPlayer()
        {
            for (int i = 0; i < 2; i++)
            {
                Assert.IsTrue(game.Join(new LogPlayer()));
            }
            Assert.AreEqual(game.maxPlayer, game.PlayerCount);
            Assert.IsFalse(game.Join(new LogPlayer()));
            Assert.AreEqual(game.maxPlayer, game.PlayerCount);
        }
        [Test]
        public void testRemovePlayer()
        {
            var player0 = new Player();
            game.Join(player0);
            game.Remove(player0);
            Assert.AreEqual(-1, player0.PlayerId);
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(0, player0.GameId);
            Assert.IsFalse(game.ContainPlayer(player0));
        }
        [Test]
        public void testGameStartException()
        {
            var exp = Assert.Throws<GameException>(() =>
            {
                game.Start();
            });
            Assert.AreEqual("No enough player for start", exp.Message);
        }
        [Test]
        public void testPlayerOutLineOnStop()
        {
            var player0 = new Player();
            game.Join(player0);
            player0.OutLine();
            Assert.AreEqual(0, game.PlayerCount);
        }
        [Test]
        public void testPlayerOutLineOnRunning()
        {
            for (int c = 0; c < 2; c++)
            {
                var players = new Player[] { new Player(), new Player() };
                for (int i = 0; i < players.Length; i++)
                {
                    game.Join(players[i]);
                }
                game.Start();
                players[0].OutLine();
                Assert.AreEqual(game.maxPlayer, game.PlayerCount);
                players[1].OutLine();
                Assert.AreEqual(0, game.PlayerCount);
                Assert.IsFalse(game.IsRunning);
            }
        }
    }
}
