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
        Room room;
        [SetUp]
        public void SetUp()
        {
            player = new Player();
            room = new Room();
            room.Join(player);
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
            room = new Room();
            room.Join(player);
            Assert.AreEqual(ResultDefine.GameNotStart, player.Play(0, 0));
        }
        [Test]
        public void testFinish()
        {
            player.Reset();
            Assert.AreEqual(0, player.chess);
            Assert.AreEqual(0, player.RoomId);
            Assert.AreEqual(-1, player.PlayerId);
            Assert.DoesNotThrow(() =>
            {
                player.Play(0, 0);
            });
        }
        [Test]
        public void testOutLine()
        {
            var player = new Player();
            Assert.DoesNotThrow(() =>
            {
                player.OutLine();
            });
        }
        [Test]
        public void testOutLineInGame()
        {
            player.notifier = new NetNotifier(new LogSocket(), player);
            player.OutLine();
            Assert.IsInstanceOf<NoneNotifier>(player.notifier);
            Assert.IsFalse(room.ContainPlayer(player));
        }
    }
}
