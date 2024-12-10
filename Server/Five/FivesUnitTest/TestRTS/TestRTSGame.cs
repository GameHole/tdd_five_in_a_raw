using Five;
using Five.RTS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest.RTS
{
    internal class TestRTSGame
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
        public void testStart()
        {
            Assert.Pass();
        }
        [Test]
        public void testGameStart()
        {
            var game = gameFact.Factroy() as RTSGame;
            var room = new TRoom();
            game.Init(room);
            Assert.AreEqual(0, game.charaters.Count);
            room.players = new Player[2];
            game.Start();
            Assert.AreEqual(2, game.charaters.Count);
        }
    }
}
