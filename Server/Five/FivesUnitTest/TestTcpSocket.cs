using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TestTcpSocket
    {
        Server server;
        TcpSocket socket;
        [SetUp]
        public void SetUp()
        {
            server = new Server("127.0.0.1", 10000);
            socket = new TcpSocket(new SerializerRegister());
            server.StartAsync();
        }
        [TearDown]
        public async Task TearDown()
        {
            socket.Close();
            await Task.Delay(100);
            server.Stop();
        }
        [Test]
        public void testRecv()
        {
            var log = "";
            socket.onRecv = (msg) => log = $"msg:{msg.opcode}";
            var serizer = new MessageSerializer();
            new SerializerRegister().Regist(serizer);
            var stream = new ByteStream();
            serizer.Serialize(new Message(MessageCode.RequestMatch), stream);
            socket.packer.Pack(stream);
            socket.Recv();
            Assert.AreEqual("msg:1", log);
        }
        [Test]
        public void testRecvHasBrokenMessage()
        {
            var serizer = new DefaultSerializer();
            var stream = new ByteStream();
            serizer.Serialize(new Message(1), stream);
            socket.packer.Pack(stream);
            var proto = new Proto();
            proto.Write(socket.packer.stream);
            socket.packer.stream.Write<int>(5);
            socket.packer.stream.Write<short>(1);
            socket.Recv();
            Assert.AreEqual(0,socket.packer.stream.Index);
            Assert.AreEqual(10, socket.packer.stream.Count);
            Assert.IsTrue(proto.IsVailed(socket.packer.stream));
        }
        [Test]
        public void testRecvMessage()
        {
            var log = "";
            socket.onRecv = (msg) => log += $"msg:{msg.opcode}";
            var serizer = new DefaultSerializer();
            ByteStream[] serStream = new ByteStream[3];
            for (int i = 0; i < serStream.Length; i++)
            {
                serStream[i] = new ByteStream();
                serizer.Serialize(new Message(1), serStream[i]);
            }
            for (int i = 0; i < serStream.Length; i++)
            {
                socket.packer.Pack(serStream[i]);
            }
            Assert.AreEqual(12*3, socket.packer.stream.Count);
            socket.Recv();
            Assert.AreEqual($"msg:1msg:1msg:1", log);

        }
        [Test]
        public async Task testSend()
        {
            TcpSocket ssocket = null;
            server.onAccept += (socket) => ssocket = socket;
            await Task.Delay(200);
            socket.Connect("127.0.0.1", 10000);
            await Task.Delay(200);
            var log = "";
            ssocket.onRecv = (msg) => log += $"msg:{msg.opcode}";
            socket.Send(new Message(MessageCode.RequestMatch));
            await Task.Delay(100);
            Assert.AreEqual("msg:1", log);
        }
        [Test]
        public async Task testClose()
        {
            TcpSocket ssocket = null;
            server.onAccept += (socket) => ssocket = socket;
            var socket = new TcpSocket(new SerializerRegister());
            await Task.Delay(200);
            socket.Connect("127.0.0.1", 10000);
            await Task.Delay(200);
            var log = "";
            ssocket.onClose = () => log += $"close";
            socket.Close();
            await Task.Delay(100);
            Assert.AreEqual("close", log);
            Assert.IsFalse(server.sockets.Contains(ssocket));
            Assert.IsFalse(ssocket.isVailed);
            Assert.Throws<ObjectDisposedException>(()=>
            {
                ssocket.Send(new Message(1));
            });
        }
    }
}
