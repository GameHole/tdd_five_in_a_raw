using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    internal class TestServerProcesser
    {
        [Test]
        public void test()
        {
            var sp = new ServerProcesser(default);
            Assert.DoesNotThrow(() => sp.OnConnect(new LogSocket()));
            Assert.DoesNotThrow(() => sp.OnStop());
        }
    }
}
