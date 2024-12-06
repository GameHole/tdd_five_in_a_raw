﻿using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FivesUnitTest;
using System.Threading.Tasks;

namespace ConcurrenceTest
{
    class TestMatching
    {
        private GameMgr matching;
        private PlayerRepository mgr;
        private MatchServce servce;
        private List<LogSocket> sockets;
        private List<LogPlayer> players;

        [SetUp]
        public void set()
        {
            var app = new App();
            matching = app.gameRsp;
            mgr = app.mgr;
            servce = new MatchServce(app,new GameFactroy());
            sockets = new List<LogSocket>(10000);
            players = new List<LogPlayer>(10000);
            for (int i = 0; i < 10000; i++)
            {
                var socket = new LogSocket();
                var p = LogPlayer.EmntyLog();
                sockets.Add(socket);
                players.Add(p);
                mgr.Add(socket, p);
            }
        }
        [Test]
        public async Task testMgrMatch()
        {
            await Repeat.RepeatAsync(sockets, (log) =>
            {
                servce.Match(log);
            });
            Assert.AreEqual(5000, matching.GameCount);
            for (int i = 1; i <= 5000; i++)
            {
                var game = matching.GetRoom(i);
                Assert.AreEqual(i, game.Id);
                Assert.AreEqual(game.maxPlayer, game.PlayerCount);
            }
            for (int i = 0; i < sockets.Count; i++)
            {
                Assert.AreEqual("Match Start ",players[i].log);
            }
        }
        [Test]
        public async Task testMgrCancelMatch()
        {
            await Repeat.RepeatAsync(sockets, (log) =>
            {
                servce.Match(log);
                servce.Cancel(log);
            });
            for (int i = 0; i < players.Count; i++)
            {
                Assert.IsTrue(isCancelRunning(players[i]));
            }
        }
        [Test]
        public async Task testMgrCancelMatchAsync()
        {
            List<LogSocket> sockets = new List<LogSocket>(10000);
            List<LogPlayer> matchers = new List<LogPlayer>(10000);
            for (int i = 0; i < 10000; i++)
            {
                var socket = new LogSocket();
                var p = new LogPlayer();
                sockets.Add(socket);
                matchers.Add(p);
                mgr.Add(socket, p);
            }
            await Repeat.RepeatAsync(sockets, (log) =>
            {
                servce.Match(log);
            });
            await Repeat.RepeatAsync(sockets, (log) =>
            {
                servce.Cancel(log);
            });
            for (int i = 0; i < matchers.Count; i++)
            {
                Assert.IsTrue(isCancelRunning(matchers[i]));
            }
        }
        bool isCancelRunning(LogPlayer matcher)
        {
            var log = matcher.log;
            //异或表示 CancelMatch 与 Start 只能存在一个（互斥）
            return log.Contains("CancelMatch") ^ log.Contains("Start");
        }
    }
}
