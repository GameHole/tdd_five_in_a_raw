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
    }
}
