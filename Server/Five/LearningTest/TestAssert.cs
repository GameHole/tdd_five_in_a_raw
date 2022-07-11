using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningTest
{
    class TestAssert
    {
        [Test]
        public void testGreater()
        {
            Assert.Greater(12, 1);
        }
        [Test]
        public void testLess()
        {
            Assert.Less(0, 1);
        }
        [Test]
        public void testGreaterOrEqual()
        {
            Assert.GreaterOrEqual(1, 1);
            Assert.GreaterOrEqual(2, 1);
        }
    }
}
