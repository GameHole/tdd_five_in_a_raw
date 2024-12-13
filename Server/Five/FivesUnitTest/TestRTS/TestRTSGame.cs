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
        private Player[] ps;
        private LogNotifier[] ntfs;

        [SetUp]
        public void set()
        {
            var gameFact = new RTGGameFactroy();
            game = gameFact.Factroy() as RTSGame;
            var room = new TRoom();
            ps = new Player[2];
            ntfs = new LogNotifier[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                ps[i] = new Player(i + 1);
                ps[i].notifier = ntfs[i] = new LogNotifier();
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
                var ch = game.charaters[i+1];
                Assert.AreEqual(i+1, ch.Id);
                Assert.AreEqual((i+1) * 10, ch.x);
                Assert.AreEqual(i + 2, ch.y);
            }
        }
        [Test]
        public void testStartNotify()
        {
            game.Start();
            for (int i = 0; i < ntfs.Length; i++)
            {
                var ntf = ntfs[i];
                Assert.AreEqual($"Send opcode:7(1,10,2)(2,20,3)(20,22.5) ", ntf.log);
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
                Assert.AreEqual(i+1, ps[i].Id);
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
            var player = new Player();
            Assert.IsInstanceOf<NoneNotifier>(player.notifier);
            var ntf = player.notifier;
            player.Reset();
            Assert.AreSame(ntf, player.notifier);
        }
        [Test]
        public void testPlayerInput()
        {
            game.Start();
            ps[0].Commit(new MoveTo { x = 1, y = 2 });
            Assert.AreEqual(1, game.charaters[1].targetX);
            Assert.AreEqual(2, game.charaters[1].targetY);
            game.Commit(new MoveTo { x = 2, y = 3 }, ps[0]);
            Assert.AreEqual(2, game.charaters[1].targetX);
            Assert.AreEqual(3, game.charaters[1].targetY);
        }
    }
}
