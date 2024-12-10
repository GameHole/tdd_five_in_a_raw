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
        Five.Client client;
        [SetUp]
        public void SetUp()
        {
            var factroy = new NetFactroy(new SerializerRegister(),new SocketFactroy());
            client = factroy.NewClient();
            accepter = new TAccepter();
            var log = new LogRequestRegister(new MatchServce( accepter));
            server = factroy.NewServer("127.0.0.1", TestApp.port, log);// new Server(net, log);
            server.StartAsync();
        }
        [TearDown]
        public async Task TearDown()
        {
            client.Close();
            await Task.Delay(100);
            server.Stop();
        }
        [Test]
        public void testSocket()
        {
            Assert.IsInstanceOf<TcpSocket>(client.socket);
        }
        [Test]
        public void testRecv()
        {
            LogProcesser log = LogProcesser.mockProcesser(client);
            var serizer = new MessageSerializer();
            new SerializerRegister().Regist(serizer);
            var stream = new ByteStream();
            serizer.Serialize(new Message(MessageCode.RequestMatch), stream);
            client.packer.Pack(stream);
            client.Recv();
            Assert.AreEqual(1, log.msg.opcode);
        }
        [Test]
        public void testRecvHasBrokenMessage()
        {
            var serizer = new DefaultSerializer();
            var stream = new ByteStream();
            serizer.Serialize(new Message(1), stream);
            client.packer.Pack(stream);
            var proto = new Proto();
            proto.Write(client.packer.recevingStream);
            client.packer.recevingStream.Write<int>(5);
            client.packer.recevingStream.Write<short>(1);
            client.Recv();
            Assert.AreEqual(0,client.packer.recevingStream.Index);
            Assert.AreEqual(10, client.packer.recevingStream.Count);
            Assert.IsTrue(proto.IsVailed(client.packer.recevingStream));
        }
        [Test]
        public void testRecvMessage()
        {
            LogProcesser log = LogProcesser.mockProcesser(client);
            var serizer = new DefaultSerializer();
            ByteStream[] serStream = new ByteStream[3];
            for (int i = 0; i < serStream.Length; i++)
            {
                serStream[i] = new ByteStream();
                serizer.Serialize(new Message(1), serStream[i]);
            }
            for (int i = 0; i < serStream.Length; i++)
            {
                client.packer.Pack(serStream[i]);
            }
            Assert.AreEqual(12 * 3, client.packer.recevingStream.Count);
            client.Recv();
            Assert.AreEqual(3, log.msgs.Count);
            Assert.AreSame(client, log.socket);
            for (int i = 0; i < log.msgs.Count; i++)
            {
                Assert.AreEqual(1, log.msgs[i].opcode);
            }
        }

        [Test]
        public async Task testSend()
        {
            await Task.Delay(200);
            client.Connect("127.0.0.1", TestApp.port);
            await Task.Delay(200);
            LogProcesser log = LogProcesser.mockServerClient(accepter.ssocket);
            client.Send(new Message(MessageCode.RequestMatch));
            await Task.Delay(100);
            Assert.AreEqual(1, log.msg.opcode);
        }

      

        [Test]
        public async Task testClose()
        {
            await Task.Delay(200);
            client.Connect("127.0.0.1", TestApp.port);
            await Task.Delay(200);
            var log = "";
            accepter.ssocket.onClose += () => log += $"close";
            client.Close();
            await Task.Delay(100);
            Assert.AreEqual("close", log);
            Assert.IsFalse(server.clients.Contains(accepter.ssocket));
            Assert.IsFalse(accepter.ssocket.isVailed);
            Assert.Throws<ObjectDisposedException>(()=>
            {
                accepter.ssocket.Send(new Message(1));
            });
        }
    }
}
