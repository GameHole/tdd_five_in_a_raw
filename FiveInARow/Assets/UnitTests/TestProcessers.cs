using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class TestProcessers
    {
        [Test]
        public void testPlayed()
        {
            var processer = new PlayedProcesser();
            processer.Process(new PlayedNotify(1, 1, 0));
            Assert.AreEqual();
        }
    }
}
