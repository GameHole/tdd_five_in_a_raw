using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TimerDriverTest
    {
        [SetUp]
        public void SetUp()
        {
            TimerDriver.Clear();
        }
        [Test]
        public void testClear()
        {
            Assert.AreEqual(0, TimerDriver.TimerCount);
            var timer0 = TimerDriver.New(2);
            var timer1 = TimerDriver.New(2);
            Assert.IsTrue(TimerDriver.IsContainTimer(timer0));
            Assert.IsTrue(TimerDriver.IsContainTimer(timer1));
            TimerDriver.Clear();
            Assert.AreEqual(0, TimerDriver.TimerCount);
            Assert.IsFalse(TimerDriver.IsContainTimer(timer0));
            Assert.IsFalse(TimerDriver.IsContainTimer(timer1));

        }
        [Test]
        public async Task testNewTimer()
        {
            LoopTimer timer = TimerDriver.New(2);
            await Task.Delay(1100);
            Assert.AreEqual(1, timer.addingUpTime);
            await Task.Delay(1100);
            Assert.AreEqual(0, timer.addingUpTime);
        }
        [Test]
        public async Task testStartTimer()
        {
            LoopTimer timer = new LoopTimer(2);
            Assert.IsTrue(TimerDriver.Start(timer));
            Assert.IsFalse(TimerDriver.Start(timer));
            await Task.Delay(1100);
            Assert.AreEqual(1, timer.addingUpTime);
            await Task.Delay(1100);
            Assert.AreEqual(0, timer.addingUpTime);
        }
        [Test]
        public async Task testStopTimer()
        {
            LoopTimer timer = TimerDriver.New(2);
            await Task.Delay(1100);
            Assert.AreEqual(1, timer.addingUpTime);
            TimerDriver.Stop(timer);
            Assert.IsFalse(TimerDriver.IsContainTimer(timer));
            await Task.Delay(1100);
            Assert.AreEqual(1, timer.addingUpTime);
        }
    }
}
