﻿using Five;
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
        private Domain domain;
        private RoomRepository roomRsp;
        private PlayerRepository playerRsp;
        private MatchServce servce;
        private List<Player> players;
        int count = 10000;
        [SetUp]
        public void set()
        {
            var fact = new TGameFactroy();
            domain = new Domain(fact, new IdGenrator());
            roomRsp = domain.roomRsp;
            playerRsp = domain.playerRsp;
            servce = domain.matchServce;
            players = new List<Player>(count);
            for (int i = 0; i < count; i++)
            {
                var p = new Player(i);
                players.Add(p);
                playerRsp.Add(p);
            }
        }
        [Test]
        public async Task testMatch()
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
                Assert.IsTrue(room.IsRunning);
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
            AssertCanceledOrStarted();
        }

        private void AssertCanceledOrStarted()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].RoomId != 0)
                    Assert.IsTrue(roomRsp.GetRoom(players[i].RoomId).IsRunning);
            }
        }

        [Test]
        public async Task testCancelMatchAsync()
        {
            await Repeat.RepeatAsync(count, (i) =>
            {
                servce.Match(i);
            });
            await Repeat.RepeatAsync(count, (i) =>
            {
                servce.Cancel(i);
            });
            AssertCanceledOrStarted();
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
            loginSvc.Init(domain);
            playerRsp.Stop();
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
            Assert.AreEqual(sockets.Count, playerRsp.Count);
            AssertCo.AssertSequence(sockets.ToArray(),(a)=>a.Id);
            await Repeat.RepeatAsync(sockets, (socket) =>
            {
                socket.Close();
            });
            AssertCo.AssertSequence(sockets.ToArray(), (a) => a.Id);
        }
        [Test]
        public async Task testPlay()
        {
            await Repeat.RepeatAsync(count, (i) =>
            {
                servce.Match(i);
                servce.Cancel(i);
                domain.playServce.Commit(i, new PlayMessage { x = i % 2 });
            });
        }
    }
}
