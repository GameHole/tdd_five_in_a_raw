using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TestGameFlow
    {
        Room room;
        Game game;
        LogPlayer[] players;
        LogNotifier[] ntfs;
        [SetUp]
        public void SetUp()
        {
            room = new RoomRepository(new GameFactroy()).NewRoom();
            game = room.game as Game;
            players = new LogPlayer[2];
            ntfs = new LogNotifier[2];
            for (int i = 0; i < players.Length; i++)
            {
                var player = LogPlayer.EmntyLog();
                ntfs[i] = new LogNotifier();
                player.notifier = ntfs[i];
                room.Join(player);
                players[i] = player;
            }
            room.Start();
        }
        [Test]
        public void testGameStartRepeet()
        {
            Assert.AreEqual(0, game.turn.index);
            room.Start();
            game.timer.Update(30);
            Assert.AreEqual(1, game.turn.index);
        }
        [Test]
        public async Task testDrivedTimer()
        {
            game.timer.time = 2;
            await Task.Delay(2100);
            Assert.AreEqual(1, game.turn.index);
        }
        [Test]
        public void testTimeOut()
        {
            game.timer.Update(game.timer.time);
            Assert.AreEqual(1, game.turn.index);
            Assert.AreEqual(0, game.chessboard.Count);
            players[0].Commit(new PlayRequest());
            Assert.AreEqual(0, game.chessboard.GetValue(0, 0));
            Assert.AreEqual(0, game.chessboard.Count);
            players[1].Commit(new PlayRequest());
            Assert.AreEqual(0, game.turn.index);
            Assert.AreEqual(1, game.chessboard.Count);
        }
        [Test]
        public async Task testPlayToFinish()
        {
            game.timer.time = 1;
            var chess = players[0].chess;
            game.chessboard.AddValue(0,0, chess);
            game.chessboard.AddValue(1, 0, chess);
            game.chessboard.AddValue(2, 0, chess);
            game.chessboard.AddValue(3, 0, chess);
            players[0].Commit(new PlayRequest { x = 4, y = 0 });
            Assert.AreEqual("Start Play(4,0) Reset ", players[0].log);
            Assert.AreEqual("Start Reset ", players[1].log);
            Assert.AreEqual(0, game.turn.index);
            players[1].Commit(new PlayRequest { x = 10, y = 10 });
            Assert.AreEqual("Start Play(4,0) Reset ", players[0].log);
            Assert.AreEqual(0, game.turn.index);
            Assert.AreEqual(0, game.chessboard.GetValue(10,10));
            await Task.Delay(1100);
            Assert.AreEqual(0, game.turn.index);
        }
        [Test]
        public void testFinish()
        {
            game.chessboard.AddValue(0, 0, 1);
            game.Finish(1);
            Assert.IsFalse(room.IsRunning);
            Assert.AreEqual(0, room.PlayerCount);
            foreach (var item in players)
            {
                Assert.AreEqual(-1, item.PlayerId);
                Assert.AreEqual(0, item.RoomId);
            }
            Assert.IsFalse(TimerDriver.IsContainTimer(game.timer));
            Assert.AreEqual(0, game.chessboard.Count);
        }
        [Test]
        public void testPlay()
        {
            var result = players[0].Commit(new PlayRequest { x = 1 });
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual(1, game.chessboard.GetValue(1, 0));
            Assert.AreEqual(1, game.turn.index);
            result = players[0].Commit(new PlayRequest { x = 2 });
            Assert.AreEqual(ResultDefine.NotCurrentTurnPlayer, result);
            Assert.AreEqual(0, game.chessboard.GetValue(2, 0));
            Assert.AreEqual(1, game.turn.index);
        }
        [Test]
        public void testNotify()
        {
            Assert.AreEqual("Start(0,1)(1,2) Turn id:0", ntfs[0].log);
            Assert.AreEqual(ntfs[0].log, ntfs[1].log);
            players[0].Commit(new PlayRequest { x = 1, y = 0 });
            Assert.AreEqual("Start(0,1)(1,2) Turn id:0 Played(1,0)id:0 Turn id:1", ntfs[0].log);
            game.Finish(0);
            Assert.AreEqual("Start(0,1)(1,2) Turn id:0 Played(1,0)id:0 Turn id:1 Finish:0", ntfs[0].log);
        }

        [Test]
        public void testPlayOnePlace()
        {
            game.chessboard.AddValue(1, 0, 2);
            var result = players[0].Commit(new PlayRequest { x = 1, y = 0 });
            Assert.AreEqual(ResultDefine.AllReadyHasChess, result);
            Assert.AreEqual(2, game.chessboard.GetValue(1, 0));
            Assert.AreEqual(0, game.turn.index);
        }
        [Test]
        public void testStop()
        {
            Assert.IsTrue(TimerDriver.IsContainTimer(game.timer));
            Assert.AreEqual(2, room.PlayerCount);
            room.Stop();
            Assert.IsFalse(room.IsRunning);
            Assert.AreEqual(0, room.PlayerCount);
            foreach (var item in players)
            {
                Assert.AreEqual(-1, item.PlayerId);
            }
            Assert.IsFalse(TimerDriver.IsContainTimer(game.timer));
        }
        [Test]
        public void testTurnResetTimer()
        {
            game.timer.Update(1);
            game.NextPlayer();
            Assert.AreEqual(0, game.timer.addingUpTime);
        }
    }
}
