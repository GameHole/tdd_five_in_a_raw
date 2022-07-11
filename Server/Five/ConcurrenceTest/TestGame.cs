using Five;
using FivesUnitTest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrenceTest
{
    class TestGame
    {
        [Test]
        public async Task testJoin()
        {
            var game = new Game();
            await Repeat.RepeatAsync(10000, () =>
            {
                var player = new LogPlayer();
                game.Join(player);
            });
            Assert.AreEqual(game.maxPlayer, game.PlayerCount);
        }
        [Test]
        public async Task testRemove()
        {
            var game = new Game();
            var player = new LogPlayer();
            game.Join(player);
            await Repeat.RepeatAsync(10000, () =>
            {
                game.Remove(player);
            });
            Assert.AreEqual(0, game.PlayerCount);
        }
        [Test]
        public async Task testOutLineStop()
        {
            var game = new Game();
            var player = new LogResetPlayer();
            game.Join(player);
            await Repeat.RepeatAsync(10000, () =>
            {
                player.OutLine();
            });
            Assert.AreEqual(0, game.PlayerCount);
            Assert.AreEqual("Reset Reset ", player.log);
        }
        [Test]
        public async Task testOutLineRunning()
        {
            var game = new Game();
            var player = new LogResetPlayer();
            var player1 = new LogResetPlayer();
            game.Join(player);
            game.Join(player1);
            game.Start();
            await Repeat.RepeatAsync(10000, () =>
            {
                player.OutLine();
                player.OutLine();
            });
            Assert.AreEqual("Reset Reset ", player.log);
            Assert.AreEqual(0, game.PlayerCount);
            Assert.IsFalse(game.IsRunning);
        }
    }
}
