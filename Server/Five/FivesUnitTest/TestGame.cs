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
            game.Join(player0);
            Assert.IsTrue(game.ContainPlayer(player0));
            Assert.AreEqual(0, player0.PlayerId);
            Assert.AreEqual(game, player0.game);
            Assert.AreEqual(1, game.PlayerCount);
            Assert.AreEqual(player0, game.GetPlayer(player0.PlayerId));
        }
        [Test]
        public void testRemovePlayer()
        {
            var player0 = new Player();
            game.Join(player0);
            game.Remove(player0);
            Assert.AreEqual(-1, player0.PlayerId);
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual(null, player0.game);
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
    }
}
