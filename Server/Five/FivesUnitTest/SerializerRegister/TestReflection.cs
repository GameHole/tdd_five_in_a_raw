using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Five;

namespace FivesUnitTest
{
    class TestReflection
    {
        [Test]
        public void testReflection()
        {
            var test = new TestMessageSerializerRegister();
            var codes = test.getCodes();
            int[] exp = new int[]
            {
                MessageCode.RequestMatch,
                MessageCode.RequestCancelMatch,
                MessageCode.RequestPlay,
                MessageCode.StartNotify,
                MessageCode.PlayedNotify,
                MessageCode.FinishNotify,
                MessageCode.TurnNotify
            };
            Assert.AreEqual(exp.Length, codes.Count);
            for (int i = 0; i < exp.Length; i++)
            {
                Assert.AreEqual(exp[i], codes[i]);
            }
        }
    }
}
