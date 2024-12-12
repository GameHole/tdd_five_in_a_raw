using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FivesUnitTest;
using System.Threading.Tasks;
using System.Threading;

namespace ConcurrenceTest
{
    public class TestMatching
    {
        private RoomRepository matching;
        private PlayerRepository mgr;
        private MatchServce servce;
        private List<TClient> clients;
        private List<LogPlayer> players;

        [SetUp]
        public void set()
        {
            var domain = new Domain( new GameFactroy(),new IdGenrator());
            matching = domain.roomRsp;
            mgr = domain.playerRsp;
            servce = new MatchServce(domain);
            clients = new List<TClient>(10000);
            players = new List<LogPlayer>(10000);
            for (int i = 0; i < 10000; i++)
            {
                var client = new TClient{ _id=i};
                var p = LogPlayer.EmntyLog();
                clients.Add(client);
                players.Add(p);
                mgr.Add(client.Id, p);
            }
        }
        [Test]
        public async Task testMgrMatch()
        {
            await Repeat.RepeatAsync(clients, (log) =>
            {
                servce.Match(log.Id);
            });
            Assert.AreEqual(5000, matching.GameCount);
            for (int i = 1; i <= 5000; i++)
            {
                var game = matching.GetRoom(i);
                Assert.AreEqual(i, game.Id);
                Assert.AreEqual(game.maxPlayer, game.PlayerCount);
            }
            for (int i = 0; i < clients.Count; i++)
            {
                Assert.AreEqual("Match Start ",players[i].log);
            }
        }
        [Test]
        public async Task testMgrCancelMatch()
        {
            await Repeat.RepeatAsync(clients, (log) =>
            {
                servce.Match(log.Id);
                servce.Cancel(log.Id);
            });
            for (int i = 0; i < players.Count; i++)
            {
                Assert.IsTrue(isCancelRunning(players[i]));
            }
        }
        [Test]
        public async Task testMgrCancelMatchAsync()
        {
            await Repeat.RepeatAsync(clients, (log) =>
            {
                servce.Match(log.Id);
            });
            await Repeat.RepeatAsync(clients, (log) =>
            {
                servce.Cancel(log.Id);
            });
            for (int i = 0; i < players.Count; i++)
            {
                Assert.IsTrue(isCancelRunning(players[i]));
            }
        }
        bool isCancelRunning(LogPlayer matcher)
        {
            var log = matcher.log;
            //异或表示 CancelMatch 与 Start 只能存在一个（互斥）
            return log.Contains("CancelMatch") ^ log.Contains("Start");
        }
        [Test]
        public async Task testIdGenrator()
        {
            var gen = new IdGenrator();
            int count = 100000;
            var array = new int[count];
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = gen.Genrate();
            });
            Assert.AreEqual(count, gen.Seed);
            AssertCo.AssertSequence(array);
        }
        [Test]
        public async Task testLogin()
        {
            var loginSvc = new ConnectProcesser();
            loginSvc.Init(servce);
            mgr.Stop();
            List<LogSocket> sockets = new List<LogSocket>(10000);
            for (int i = 0; i < 10000; i++)
            {
                var socket = new LogSocket();
                sockets.Add(socket);
            }
            await Repeat.RepeatAsync(sockets, (socket) =>
            {
                loginSvc.Process(socket,default);
            });
            Assert.AreEqual(sockets.Count, mgr.Count);
            AssertCo.AssertSequence(sockets.ToArray(),(a)=>a.Id);
            await Repeat.RepeatAsync(sockets, (socket) =>
            {
                socket.Close();
            });
            AssertCo.AssertSequence(sockets.ToArray(), (a) => a.Id);
        }
    }
}
