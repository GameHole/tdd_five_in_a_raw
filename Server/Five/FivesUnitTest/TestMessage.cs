using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestMessage
    {
        [Test]
        public void testMessage()
        {
            var message = new Message(1);
            Assert.AreEqual(1, message.opcode);
        }
        [Test]
        public void testToString()
        {
            var message = new Message(1);
            Assert.AreEqual("opcode:1", message.ToString());
        }
        [Test]
        public void testResponse()
        {
            var msg = new Message(1);
            var message = new Response().SetInfo(msg, new Result(1));
            Assert.AreEqual(msg.opcode + 1, message.opcode);
            Assert.AreEqual(1, message.result);
        }
        [Test]
        public void testResToString()
        {
            var message = new Response().SetInfo(new Message(1), new Result(1));
            Assert.AreEqual("opcode:2 result:1", message.ToString());
        }
    }
}
