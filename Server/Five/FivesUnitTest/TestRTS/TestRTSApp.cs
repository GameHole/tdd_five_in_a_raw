using Five;
using Five.RTS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest.RTS
{
    internal class TestRTSApp
    {
        private Client[] clients;
        private Server server;
        private RTGGameFactroy gameFact;

        [SetUp]
        public void set()
        {
            var net = new NetFactroy(new RTSSerializerRegister(),new SocketFactroy());
            gameFact = new RTGGameFactroy();
            var domain = new Domain(gameFact, new IdGenrator());
            var servce = new MatchServce(domain);
            server = net.NewServer("127.0.0.1", 12000, new RTSProcessFactroy(servce));
            server.StartAsync();
            clients = new Client[2];
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i] = net.NewClient();
            }
        }
        [TearDown]
        public void tear()
        {
            server.Stop();
            foreach (var item in clients)
            {
                item.Close();
            }
        }
        [Test]
        public async Task testStart()
        {
            await Task.Delay(200);
            clients[0].Connect("127.0.0.1", 12000);
            await Task.Delay(200);
            var code = MessageCode.GetResponseCode(MessageCode.RequestMatch);
            var log = LogProcesser.mockProcesser(clients[0], code);
            clients[0].Send(new Message { opcode = MessageCode.RequestMatch });
            await Task.Delay(200);
            Assert.AreEqual(code, log.msg.opcode);

        }
    }
}
