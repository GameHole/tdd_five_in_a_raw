using Five;
using FivesUnitTest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrenceTest
{
    class TestRoom
    {
        private Room room;

        [SetUp]
        public void set()
        {
            room = new GameMgr().NewRoom(new GameFactroy().Factroy());
        }
        [Test]
        public async Task testJoin()
        {
            await Repeat.RepeatAsync(10000, () =>
            {
                var player = new LogPlayer();
                room.Join(player);
            });
            Assert.AreEqual(room.maxPlayer, room.PlayerCount);
        }
        [Test]
        public async Task testRemove()
        {
            var player = new LogPlayer();
            room.Join(player);
            await Repeat.RepeatAsync(10000, () =>
            {
                room.Remove(player);
            });
            Assert.AreEqual(0, room.PlayerCount);
        }
        [Test]
        public async Task testOutLineStop()
        {
            var player = new LogResetPlayer();
            room.Join(player);
            await Repeat.RepeatAsync(10000, () =>
            {
                player.OutLine();
            });
            Assert.AreEqual(0, room.PlayerCount);
            Assert.AreEqual("Reset Reset ", player.log);
        }
        [Test]
        public async Task testOutLineRunning()
        {
            var player = new LogResetPlayer();
            var player1 = new LogResetPlayer();
            room.Join(player);
            room.Join(player1);
            room.Start();
            await Repeat.RepeatAsync(10000, () =>
            {
                player.OutLine();
                player.OutLine();
            });
            Assert.AreEqual("Reset Reset ", player.log);
            Assert.AreEqual(0, room.PlayerCount);
            Assert.IsFalse(room.IsRunning);
        }
    }
}
