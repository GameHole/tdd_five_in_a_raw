using Five;
using FivesUnitTest;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
            room = new RoomRepository(new TGameFactroy()).NewRoom();
        }
        [Test]
        public async Task testJoin()
        {
            await Repeat.RepeatAsync(100000, (i) =>
            {
                var player = new Player(i);
                room.Join(player);
            });
            Assert.AreEqual(room.maxPlayer, room.PlayerCount);
            Assert.AreEqual(room.maxPlayer, room.Players.Count());
        }
        [Test]
        public async Task testConcurrentDictionaryCount()
        {
            var c = new ConcurrentDictionary<int, int>(); 
            await Repeat.RepeatAsync(100000, (i) =>
            {
                if (c.Count < 2)
                    c.TryAdd(i, i);
            });
            Assert.AreNotEqual(2, c.Count);
        }
        [Test]
        public async Task testIsFull()
        {
            await Repeat.RepeatAsync(200000, (i) =>
            {
                var player = new Player(i);
                room.Join(player);
            });
            Assert.AreEqual(room.maxPlayer, room.PlayerCount);
            Assert.AreEqual(room.maxPlayer, room.Players.Count());
            Assert.IsTrue(room.isFull());
        }
        [Test]
        public async Task testRemove()
        {
            var player = new Player();
            room.Join(player);
            await Repeat.RepeatAsync(100000, (i) =>
            {
                room.Remove(player);
            });
            Assert.AreEqual(0, room.PlayerCount);
        }
        [Test]
        public async Task testOutLineStop()
        {
            var player = new LogResetPlayer(1);
            room.Join(player);
            await Repeat.RepeatAsync(10000, (i) =>
            {
                player.OutLine();
            });
            Assert.AreEqual(0, room.PlayerCount);
            Assert.AreEqual("Reset Reset ", player.log);
        }
        [Test]
        public async Task testOutLineRunning()
        {
            var player = new LogResetPlayer(1);
            var player1 = new LogResetPlayer(2);
            room.Join(player);
            room.Join(player1);
            room.Start();
            await Repeat.RepeatAsync(10000, (i) =>
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
