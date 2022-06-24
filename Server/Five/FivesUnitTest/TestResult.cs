using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestResult
    {
        [Test]
        public void testResult()
        {
            var result = new Result(1);
            Assert.AreEqual(new Result(1), result);
            Assert.AreNotEqual(1, result);
        }
        [Test]
        public void testEqual()
        {
            var result = new Result(1);
            Assert.IsFalse(new Result(1).Equals(null));
            Assert.IsFalse(new Result(1).Equals(1));
            Assert.IsTrue(new Result(1).Equals(result));
            Assert.IsTrue(new Result(1) == result);
            Assert.IsFalse(new Result(2).Equals(result));
            Assert.IsFalse(new Result(2) == result);
        }
    }
}
