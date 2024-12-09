using ConcurrenceTest;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearningTest
{
    public class TestInterlocked
    {
       

        [Test]
        public void testCompareExchange()
        {
            int local = 1;
            Assert.AreEqual(1, Interlocked.CompareExchange(ref local, 0, 2));
            Assert.AreEqual(1, local);
            Assert.AreEqual(1, Interlocked.CompareExchange(ref local, 0, 1));
            Assert.AreEqual(0, local);
        }
        [Test]
        public async Task testInterlocked()
        {
            int count = 100000;
            int idx = 0;
            int idx1 = 0;
            await Repeat.RepeatAsync(count, (i) =>
            {
                Interlocked.Increment(ref idx);
                Interlocked.Increment(ref idx1);
            });
            Assert.AreEqual(count, idx);
            Assert.AreEqual(count, idx1);
        }
        [Test]
        public void testInterlockTime()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            int count = 1000000;
            stopwatch.Start();
            for (int i = 0; i < count; i++) { }
            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            stopwatch.Start();
            for (int i = 0; i < count; Interlocked.Increment(ref i)) { }
            stopwatch.Stop();
            var rate = stopwatch.Elapsed / time;
            Console.WriteLine(rate);
            Assert.LessOrEqual(rate, 15);
            Assert.GreaterOrEqual(rate, 9);
        }
    }
}