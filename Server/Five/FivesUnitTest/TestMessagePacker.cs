using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMessagePacker
    {
        ByteStream stream;
        MessagePacker packer;
        Proto proto;
        [SetUp]
        public void SetUp()
        {
            stream = new ByteStream();
            stream.Write(1);
            proto = new Proto();
            packer = new MessagePacker(proto);
        }
        [Test]
        public void testPack()
        {
            int count = 2;
            for (int i = 0; i < count; i++)
            {
                packer.Pack(stream);
            }
            Assert.AreEqual(12* count, packer.stream.Count);
            for (int i = 0; i < count; i++)
            {
                int mark = packer.stream.Read<int>();
                Assert.AreEqual(proto.proto, mark);
                int len = packer.stream.Read<int>();
                Assert.AreEqual(4, len);
                int message = packer.stream.Read<int>();
                Assert.AreEqual(1, message);
            }
        }
        [Test]
        public void testUnpack()
        {
            ByteStream[] streams = new ByteStream[2];
            for (int i = 0; i < streams.Length; i++)
            {
                streams[i] = new ByteStream();
                streams[i].Write(i + 1);
                packer.Pack(streams[i]);
            }
            ByteStream outstream;
            for (int i = 0; i < streams.Length; i++)
            {
                Assert.IsTrue(packer.Unpack(out outstream));
                Assert.AreEqual(4, outstream.GetLastCount());
                Assert.AreEqual(i+1, outstream.Read<int>());
            }
            Assert.IsFalse(packer.Unpack(out outstream));
        }
        [Test]
        public void testUnpackInsertedSomeInvalidData()
        {
            packer.Pack(stream);
            packer.stream.Write<int>(1);
            packer.stream.Write<int>(3);
            packer.Pack(stream);
            for (int i = 0; i < 2; i++)
            {
                Assert.IsTrue(packer.Unpack(out ByteStream outstream),$"loop:{i}");
                Assert.AreEqual(1, outstream.Read<int>());
            }
        }
        [Test]
        public void testUnpackWithBrokenProto()
        {
            packer.stream.Write<short>(20);
            Assert.IsFalse(packer.Unpack(out ByteStream outstream));
            Assert.AreEqual(0, packer.stream.Index);
        }
        [Test]
        public void testUnpackWithNoLength()
        {
            proto.Write(packer.stream);
            Assert.IsFalse(packer.Unpack(out ByteStream outstream));
            Assert.AreEqual(0, packer.stream.Index);
        }
        [Test]
        public void testUnpackWithBrokenLength()
        {
            proto.Write(packer.stream);
            packer.stream.Write<short>(0);
            Assert.IsFalse(packer.Unpack(out ByteStream outstream));
            Assert.AreEqual(0, packer.stream.Index);
        }
        [Test]
        public void testUnpackWithBrokenData()
        {
            proto.Write(packer.stream);
            packer.stream.Write<int>(3);
            packer.stream.Write<short>(2);
            Assert.IsFalse(packer.Unpack(out ByteStream outstream));
            Assert.AreEqual(0, packer.stream.Index);
        }
    }
}
