using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TimerTest
    {
        LoopTimer timer;
        [SetUp]
        public void SetUp()
        {
            timer = new LoopTimer(10);
        }
        [Test]
        public void testTimer()
        {
            Assert.AreEqual(10, timer.time);
            Assert.AreEqual(0f, timer.addingUpTime);
        }
        [Test]
        public void testEvent()
        {
            var log = "";
            timer.onTime += () =>
            {
                log += "ontime";
            };
            timer.Update(5);
            Assert.AreEqual(5, timer.addingUpTime);
            Assert.AreEqual("", log);
            timer.Update(5);
            Assert.AreEqual(0, timer.addingUpTime);
            Assert.AreEqual("ontime", log);
        }
        [Test]
        public async Task testSystemTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 100;
            var log = "";
            timer.Elapsed += (o, e) =>
            {
                log += "Elapsed";
            };
            timer.Start();
            timer.AutoReset = false;
            await Task.Delay(150);
            Assert.AreEqual("Elapsed", log);
            await Task.Delay(150);
            Assert.AreEqual("Elapsed", log);
        }
       
    }
}
