using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Five;
namespace FivesUnitTest
{
    class TestPlayer
    {
        Player player;
        Game game;
        [SetUp]
        public void SetUp()
        {
            player = new Player();
            game = new Game();
            game.Join(player);
            player.Start(1);
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
        public void testFinish()
        {
            player.Finish();
            Assert.AreEqual(0, player.chess);
            Assert.AreEqual(0, player.GameId);
            Assert.AreEqual(-1, player.PlayerId);
            Assert.DoesNotThrow(() =>
            {
                player.Play(0, 0);
            });
        }
        
    }
}
