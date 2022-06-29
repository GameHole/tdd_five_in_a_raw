using NUnit.Framework;
using System.Threading;

namespace LearningTest
{
    public class TestInterlocked
    {
       

        [Test]
        public void testCompareExchange()
        {
            int local = 1;
            Assert.AreEqual(1, Interlocked.CompareExchange(ref local, 0, 2));
            Assert.AreEqual(1, local);
            Assert.AreEqual(1, Interlocked.CompareExchange(ref local, 0, 1));
            Assert.AreEqual(0, local);
        }
    }
}