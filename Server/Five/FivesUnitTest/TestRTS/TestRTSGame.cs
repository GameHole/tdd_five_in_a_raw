using Five;
using Five.RTS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest.RTS
{
    internal class TestRTSGame
    {
        private RTSGame game;
        private LogPlayer[] ps;
        private LogRTSNotifier[] ntfs;

        [SetUp]
        public void set()
        {
            var gameFact = new RTGGameFactroy();
            game = gameFact.Factroy() as RTSGame;
            var room = new TRoom();
            ps = new LogPlayer[2];
            ntfs = new LogRTSNotifier[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                ps[i] = new LogPlayer();
                ps[i].rtsnotifier = ntfs[i] = new LogRTSNotifier();
            }
            room.players = ps;
            game.Init(room);
        }
        [Test]
        public void testStartCharaters()
        {
            Assert.AreEqual(0, game.charaters.Count);
            game.Start();
            Assert.AreEqual(2, game.charaters.Count);
            for (int i = 0; i < game.charaters.Count; i++)
            {
                var ch = game.charaters[i];
                Assert.AreEqual(i, ch.id);
                Assert.AreEqual(i, ps[i].PlayerId);
                Assert.AreEqual(i * 10, ch.x);
                Assert.AreEqual(i + 1, ch.y);
            }
        }
        [Test]
        public void testStartNotify()
        {
            game.Start();
            for (int i = 0; i < ntfs.Length; i++)
            {
                var ntf = ntfs[i];
                Assert.AreEqual($"Start(0,0,1)(1,10,2)(20,22.5)", ntf.log);
            }
        }
        [Test]
        public void testStop()
        {
            game.Start();
            game.Stop();
            Assert.AreEqual(0, game.charaters.Count);
            for (int i = 0; i < ps.Length; i++)
            {
                Assert.AreEqual(i, ps[i].PlayerId);
            }
        }

        [Test]
        public void testStartFinishPoint()
        {
            game.Start();
            Assert.AreEqual(20,game.finishX);
            Assert.AreEqual(22.5f, game.finishY);
        }
        [Test]
        public void testReset()
        {
            var player = new LogPlayer();
            Assert.IsInstanceOf<NoneRTSNotifier>(player.rtsnotifier);
            var ntf = player.rtsnotifier;
            player.Reset();
            Assert.AreSame(ntf, player.rtsnotifier);
            game.Start();
            ps[0].Reset();
            Assert.AreEqual(-1, ps[0].PlayerId);
        }
        [Test]
        public void testPlayerInput()
        {
            game.Start();
            ps[0].Commit(new MoveToMessage { x = 1, y = 2 });
            Assert.AreEqual(1, game.charaters[0].targetX);
            Assert.AreEqual(2, game.charaters[0].targetY);
            game.Commit(new MoveToMessage { x = 2, y = 3 }, ps[0]);
            Assert.AreEqual(2, game.charaters[0].targetX);
            Assert.AreEqual(3, game.charaters[0].targetY);
        }
    }
}
