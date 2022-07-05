using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class TestNoneResponseProcesser
    {
        [Test]
        public void test()
        {
            var none = new NoneResponseProcesser(1);
            Assert.AreEqual(MessageCode.GetResponseCode(1), none.OpCode);
        }
    }
}
