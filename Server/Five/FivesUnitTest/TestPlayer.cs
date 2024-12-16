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
        }
        [Test]
        public void testNoGamePlay()
        {
            var player = new Player();
            Assert.AreEqual(ResultDefine.PlayerNotInTheGame, player.Commit(new PlayMessage()));
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
        public void testPlayable()
        {
            var room = new TRoom();
            room.players = new Player[] { player };
            var test = new TGame();
            test.Init(room);
            test.Start();
            var msg = new Message();
            Assert.AreEqual(-1, player.Commit(msg).code);
            Assert.AreSame(msg, test.msg);
            Assert.AreSame(player, test.player);
        }
        [Test]
        public void testFinish()
        {
            player.Reset();
            Assert.AreEqual(0, player.RoomId);
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
