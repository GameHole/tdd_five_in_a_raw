using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Five;
namespace FivesUnitTest
{
    class PlayerTest
    {
        Player player;
        Game game;
        [SetUp]
        public void SetUp()
        {
            player = new Player();
            game = new Game();
            game.Join(player);
            player.Start(game, 1);
        }

        [Test]
        public void testJoinedPlayer()
        {
            Assert.AreEqual(1,player.chess);
        }
        [Test]
        public void testNoGamePlay()
        {
            var player = new Player();
            Assert.AreEqual(ResultDefine.PlayerNotInTheGame, player.Play(0,0));
        }
        [Test]
        public void testNotStartPlay()
        {
            player = new Player();
            game = new Game();
            game.Join(player);
            Assert.AreEqual(ResultDefine.GameNotStart, player.Play(0, 0));
        }
        [Test]
        public void testPlay()
        {
            player.Play(1,0);
            Assert.AreEqual(1, game.chessboard.GetValue(1,0));
            Assert.AreEqual(1, game.turn.index);
            player.Play(2, 0);
            Assert.AreEqual(0, game.chessboard.GetValue(2, 0));
            Assert.AreEqual(1, game.turn.index);
        }
        [Test]
        public void testPlayResult()
        {
            var result = player.Play(1, 0);
            Assert.AreEqual(ResultDefine.Success, result);
            result = player.Play(1, 0);
            Assert.AreEqual(ResultDefine.PlayerIsNotThis, result);
        }
        [Test]
        public void testPlayOnePlace()
        {
            game.chessboard.AddValue(1, 0, 2);
            player.Play(1, 0);
            Assert.AreEqual(2, game.chessboard.GetValue(1, 0));
            Assert.AreEqual(0, game.turn.index);
        }
        [Test]
        public void testFinish()
        {
            player.Finish();
            Assert.AreEqual(0, player.chess);
            Assert.AreEqual(null, player.game);
            Assert.AreEqual(-1, player.PlayerId);
            Assert.DoesNotThrow(() =>
            {
                player.Play(0, 0);
            });
        }
    }
}
