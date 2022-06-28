using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestByteStream
    {
        ByteStream stream;
        [SetUp]
        public void SetUp()
        {
            stream = new ByteStream();
        }
        [Test]
        public void testByteStream()
        {
            Assert.AreEqual(0, stream.Index);
            Assert.AreEqual(0, stream.Count);
        }
        [Test]
        public void testByteStreamCapcity()
        {
            var stream = new ByteStream(4);
            Assert.AreEqual(4, stream.Bytes.Length);
            Assert.AreEqual(0, stream.Count);
            Assert.AreEqual(0, stream.Index);
        }
        [Test]
        public void testReadException()
        {
           var ex= Assert.Throws<StreamException>(() => stream.Read<int>());
            Assert.AreEqual("No enought length for reading type. Need 4,but last 0", ex.Message);
        }
        [Test]
        public void testResetIndex()
        {
            stream.Write<int>(1);
            stream.Read<int>();
            Assert.AreNotEqual(0, stream.Index);
            stream.ResetIndex();
            Assert.AreEqual(0, stream.Index);
        }
        [Test]
        public void testWrite()
        {
            stream.Write<int>(1);
            Assert.AreEqual(sizeof(int), stream.Count);
        }
        [Test]
        public void testRead()
        {
            stream.Write<int>(1);
            stream.Write<int>(2);
            Assert.AreEqual(1, stream.Read<int>());
            Assert.AreEqual(sizeof(int), stream.Index);
            Assert.AreEqual(2, stream.Read<int>());
            Assert.AreEqual(sizeof(int)*2, stream.Index);
        }
    }
}
