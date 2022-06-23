using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class MessageTest
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
        public void testResToString()
        {
            var message = new Response(new Message(1),new Result(1));
            Assert.AreEqual("opcode:1 result:1", message.ToString());
        }
    }
}
