using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestTcpSocket
    {
        [Test]
        public void testRecv()
        {
            TcpSocket socket = new TcpSocket();
            var log = "";
            socket.onRecv = (msg) => log = $"msg:{msg.opcode}";
            var serizer = new MessageSerializer();
            var stream = new ByteStream();
            serizer.Serialize(new Message(MessageCode.RequestMatch), stream);
            socket.Recv(stream);
            Assert.AreEqual("msg:1", log);
        }
        [Test]
        public void testSend()
        {
            Assert.Fail();
        }
    }
}
