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
            Assert.IsFalse(proto.IsVailed(stream));
            proto.Write(stream);
            Assert.AreEqual(proto.ByteSize, stream.Count);
            Assert.IsTrue(proto.IsVailed(stream));
            Assert.AreEqual(0, stream.Index);
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
