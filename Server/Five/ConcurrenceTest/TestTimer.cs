using Five;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ConcurrenceTest
{
    class TestTimer
    {
        [Test]
        public async Task testReset()
        {
            var timer = new LoopTimer(10);
            await Repeat.RepeatAsync(1000000, async (i) =>
            {
                timer.Update(1);
                await Task.Delay(100);
                timer.Reset();
            });
            Assert.AreEqual(0, timer.addingUpTime);
        }
    }
}
