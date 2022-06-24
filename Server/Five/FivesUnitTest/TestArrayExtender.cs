using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestArrayExtender
    {
        [Test]
        public void testExtender()
        {
            var extend = new ArrayExtender<byte>();
            byte[] org = new byte[0];
            var exArray = extend.TryExtend(org,0,1);
            Assert.AreEqual(1, exArray.Length);
            exArray = extend.TryExtend(org, 0, 3);
            Assert.AreEqual(4, exArray.Length);
            exArray = extend.TryExtend(org, 0, 5);
            Assert.AreEqual(8, exArray.Length);
        }
    }
}
