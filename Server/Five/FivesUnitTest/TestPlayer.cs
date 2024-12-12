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
        RoomRepository factroy;
        Room room;
        [SetUp]
        public void SetUp()
        {
            player = new Player();
            factroy = new RoomRepository(new TGameFactroy());
            room = factroy.NewRoom();
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
            Assert.AreEqual(ResultDefine.PlayerNotInTheGame, player.Commit(new PlayRequest()));
        }
        [Test]
        public void testNotStartPlay()
        {
            player = new Player();
            room = factroy.NewRoom();
            room.Join(player);
            Assert.AreEqual(ResultDefine.GameNotStart, player.Commit(new PlayRequest()));
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
                player.Commit(new PlayRequest());
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
            player.notifier = new LogNotifier();
            player.OutLine();
            Assert.IsInstanceOf<NoneNotifier>(player.notifier);
            Assert.IsFalse(room.ContainPlayer(player));
        }
    }
}
