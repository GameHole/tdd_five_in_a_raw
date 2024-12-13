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
        public void testState()
        {
            Assert.AreEqual(StateDefine.Idle, player.state);
            Assert.IsTrue(player.TrySwitchStateTo(1));
            Assert.AreEqual(1, player.state);
            player.Reset();
            Assert.AreEqual(StateDefine.Idle, player.state);
        }
        [Test]
        public void testSwitchState()
        {
            Assert.IsTrue(player.TrySwitchStateTo(1));
            Assert.IsFalse(player.TrySwitchStateTo(1));
        }
        [Test]
        public void testSwitchStateForGame()
        {
            Assert.IsTrue(player.TrySwitchStateTo(StateDefine.Matching));
            Assert.IsFalse(player.TrySwitchStateTo(StateDefine.Matching));
            player.TrySwitchStateTo(StateDefine.Matching);
            Assert.IsTrue(player.TrySwitchStateTo(StateDefine.Playing));
            Assert.IsFalse(player.TrySwitchStateTo(StateDefine.Playing));
            Assert.IsFalse(player.TrySwitchStateTo(StateDefine.Matching));
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
