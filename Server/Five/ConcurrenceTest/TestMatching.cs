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
        private RoomRepository roomRsp;
        private PlayerRepository mgr;
        private MatchServce servce;
        private List<LogPlayer> players;
        int count = 10000;
        [SetUp]
        public void set()
        {
            var domain = new Domain( new GameFactroy(),new IdGenrator());
            roomRsp = domain.roomRsp;
            mgr = domain.playerRsp;
            servce = new MatchServce(domain);
            players = new List<LogPlayer>(count);
            for (int i = 0; i < count; i++)
            {
                var p = LogPlayer.EmntyLog(i);
                players.Add(p);
                mgr.Add(p);
            }
        }
        [Test]
        public async Task testMgrMatch()
        {
            await Repeat.RepeatAsync(count, (i) =>
            {
                servce.Match(i);
            });
            int roomCount = count / 2;
            Assert.AreEqual(roomCount, roomRsp.Count);
            for (int i = 1; i <= roomCount; i++)
            {
                var room = roomRsp.GetRoom(i);
                Assert.AreEqual(i, room.Id);
                Assert.AreEqual(room.maxPlayer, room.PlayerCount);
            }
            for (int i = 0; i < players.Count; i++)
            {
                Assert.AreEqual("Start ",players[i].log);
            }
        }
        [Test]
        public async Task testMgrCancelMatch()
        {
            await Repeat.RepeatAsync(count, (i) =>
            {
                servce.Match(i);
                servce.Cancel(i);
            });
            for (int i = 0; i < players.Count; i++)
            {
                Assert.IsTrue(isCancelRunning(players[i]));
            }
        }
        [Test]
        public async Task testMgrCancelMatchAsync()
        {
            await Repeat.RepeatAsync(count, (i) =>
            {
                servce.Match(i);
            });
            await Repeat.RepeatAsync(count, (i) =>
            {
                servce.Cancel(i);
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
            List<LogSocket> sockets = new List<LogSocket>(count);
            for (int i = 0; i < count; i++)
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
