using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TestDefaultClient
    {
        Server server;
        private TAccepter accepter;
        Five.DefaultClient socket;
        [SetUp]
        public void SetUp()
        {
            var factroy = new NetFactroy(new SerializerRegister());
            socket = factroy.NewClient();
            accepter = new TAccepter(new GameFactroy());
            var log = new LogRequestRegister(new MatchServce( accepter));
            server = factroy.NewServer("127.0.0.1", TestApp.port, log);// new Server(net, log);
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
        public void testSocket()
        {
            Assert.IsInstanceOf<NetTcpSocket>(socket.socket);
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
            proto.Write(socket.packer.recevingStream);
            socket.packer.recevingStream.Write<int>(5);
            socket.packer.recevingStream.Write<short>(1);
            socket.Recv();
            Assert.AreEqual(0,socket.packer.recevingStream.Index);
            Assert.AreEqual(10, socket.packer.recevingStream.Count);
            Assert.IsTrue(proto.IsVailed(socket.packer.recevingStream));
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
            Assert.AreEqual(12*3, socket.packer.recevingStream.Count);
            socket.Recv();
            Assert.AreEqual($"msg:1msg:1msg:1", log);

        }
        [Test]
        public async Task testSend()
        {
            await Task.Delay(200);
            socket.Connect("127.0.0.1", TestApp.port);
            await Task.Delay(200);
            var log = "";
            accepter.ssocket.onRecv = (msg) => log += $"msg:{msg.opcode}";
            socket.Send(new Message(MessageCode.RequestMatch));
            await Task.Delay(100);
            Assert.AreEqual("msg:1", log);
        }
        [Test]
        public async Task testClose()
        {
            await Task.Delay(200);
            socket.Connect("127.0.0.1", TestApp.port);
            await Task.Delay(200);
            var log = "";
            accepter.ssocket.onClose += () => log += $"close";
            socket.Close();
            await Task.Delay(100);
            Assert.AreEqual("close", log);
            Assert.IsFalse(server.sockets.Contains(accepter.ssocket));
            Assert.IsFalse(accepter.ssocket.isVailed);
            Assert.Throws<ObjectDisposedException>(()=>
            {
                accepter.ssocket.Send(new Message(1));
            });
        }
    }
}
