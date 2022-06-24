using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace FivesUnitTest
{
    class TestSerialize
    {
        MessageSerializer serizer;
        ByteStream stream;
        [SetUp]
        public void SetUp()
        {
            serizer = new MessageSerializer();
            stream = new ByteStream();
        }
        [Test]
        public void testSerializeMessage()
        {
            serizer.Serialize(new Message(MessageCode.RequestMatch), stream);
            Assert.AreEqual(4, stream.Count);
            Assert.AreEqual(1, stream.Read<int>());
            stream.ResetIndex();
            var msg = serizer.Deserialize(stream);
            Assert.AreEqual(1, msg.opcode);
        }
        [Test]
        public void testSerializePlayMessage()
        {
            serizer.Serialize(new PlayMessage(1,2), stream);
            var msg = serizer.Deserialize(stream);
            Assert.AreEqual(MessageCode.RequestPlay, msg.opcode);
            Assert.AreEqual(typeof(PlayMessage), msg.GetType());
            var play = msg as PlayMessage;
            Assert.AreEqual(1, play.x);
            Assert.AreEqual(2, play.y);
        }
        [Test]
        public void testSerializeResponse()
        {
            serizer.Serialize(new Response(new Message(MessageCode.RequestMatch), new Result(2)), stream);
            Assert.AreEqual(8, stream.Count);
            Assert.AreEqual(2, stream.Read<int>());
            Assert.AreEqual(2, stream.Read<int>());
            stream.ResetIndex();
            var msg = serizer.Deserialize(stream);
            Assert.AreEqual(typeof(Response), msg.GetType());
            var resp = msg as Response;
            Assert.AreEqual(2, resp.opcode);
            Assert.AreEqual(2, resp.result);
        }
    }
}
