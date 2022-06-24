using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMessageCode
    {
        [Test]
        public void Test()
        {
            Assert.AreEqual(1, MessageCode.RequestMatch);
            Assert.AreEqual(2, MessageCode.GetResponseCode(MessageCode.RequestMatch));
            Assert.AreEqual(3, MessageCode.RequestCancelMatch);
            Assert.AreEqual(5, MessageCode.RequestPlay);
        }
    }
}
