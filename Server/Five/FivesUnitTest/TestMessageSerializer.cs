using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace FivesUnitTest
{
    class TestMessageSerializer
    {
        MessageSerializer serizer;
        ByteStream stream;
        LogSerializer serializer;
        const int OpCode = 100000;
        [SetUp]
        public void SetUp()
        {
            serizer = new MessageSerializer();
            stream = new ByteStream();
            serializer = new LogSerializer();
            serizer.Container.Add(OpCode, serializer);
        }
        [Test]
        public void testContainSerializer()
        {
            Assert.IsTrue(serizer.Container.Contains(OpCode));
        }
        [Test]
        public void testRunSerializer()
        {
            serizer.Serialize(new Message(OpCode), stream);
            Assert.AreEqual("Serialize", serializer.log);
        }
        [Test]
        public void testRunDeserializer()
        {
            stream.Write(OpCode);
            serizer.Deserialize(stream);
            Assert.AreEqual("Deserialize", serializer.log);
        }
        [Test]
        public void testTryDeserializer()
        {
            stream.Write(OpCode);
            Assert.IsTrue(serizer.TryDeserialize(stream, out var message));
            Assert.AreEqual("Deserialize", serializer.log);
        }
        [Test]
        public void testFaildTryDeserializer()
        {
            stream.Write(20);
            Assert.IsFalse(serizer.TryDeserialize(stream, out var message));
        }
        [Test]
        public void testNotContainedDeserialize()
        {
            stream.Write<int>(10);
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var msg = serizer.Deserialize(stream);
            });
        }
    }
}
