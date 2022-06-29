using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FivesUnitTest;
using System.Threading.Tasks;

namespace ConcurrenceTest
{
    class TestMatching
    {
        [Test]
        public async Task testMatch()
        {
            var matching = new Matching();
            List<LogMatcher> matchers = new List<LogMatcher>(10000);
            for (int i = 0; i < 10000; i++)
            {
                matchers.Add(new LogMatcher(matching));
            }
            await Repeat.RepeatAsync(matchers, (log) =>
            {
                log.Match();
            });
            Assert.AreEqual(5000, matching.GameCount);
            for (int i = 1; i <= 5000; i++)
            {
                var game = matching.GetGame(i);
                Assert.AreEqual(i, game.Id);
                Assert.AreEqual(game.maxPlayer, game.PlayerCount);
            }
            for (int i = 0; i < matchers.Count; i++)
            {
                Assert.AreEqual("Match Start ", matchers[i].log);
            }
        }
        [Test]
        public async Task testCancelMatch()
        {
            var matching = new Matching();
            List<LogMatcher> matchers = new List<LogMatcher>(10000);
            for (int i = 0; i < 10000; i++)
            {
                matchers.Add(new LogMatcher(matching));
            }
            await Repeat.RepeatAsync(matchers, (log) =>
            {
                log.Match();
                log.Cancel();
            });
            for (int i = 0; i < matchers.Count; i++)
            {
                Assert.IsTrue(isCancelRunning(matchers[i]));
            }
        }
        bool isCancelRunning(LogMatcher matcher)
        {
            var log = matcher.log;
            //异或表示 CancelMatch 与 Start 只能存在一个（互斥）
            return log.Contains("CancelMatch") ^ log.Contains("Start");
        }
    }
}
