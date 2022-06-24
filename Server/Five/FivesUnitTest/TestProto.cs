using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestProto
    {
        [Test]
        public void testProro()
        {
            Proto proto = new Proto();
            ByteStream stream = new ByteStream();
            proto.Write(stream);
            Assert.AreEqual(4, stream.Count);
            Assert.IsTrue(proto.IsVailed(stream));
        }
        [Test]
        public void testIsVailed()
        {
            ByteStream stream = new ByteStream();
            stream.Write<int>(5);
            Assert.IsFalse(new Proto().IsVailed(stream));
        }
    }
}
