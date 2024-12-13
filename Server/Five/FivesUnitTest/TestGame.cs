using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestGame
    {
        Room room;
        Game game;
        [SetUp]
        public void SetUp()
        {
            room = new RoomRepository(new GameFactroy()).NewRoom();
            game = room.game as Game;
            room.Id = 10;
        }
        [Test]
        public void testGame()
        {
            Assert.AreEqual(0, room.PlayerCount);
            Assert.AreEqual(0, game.turn.index);
            Assert.AreEqual(30, game.timer.time);
            Assert.AreEqual(15, game.chessboard.width);
            Assert.AreEqual(15, game.chessboard.height);
        }
        [Test]
        public void testGameStart()
        {
            var players = new LogPlayer[2];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = LogPlayer.EmntyLog(i+2);
                room.Join(players[i]);
            }
            game.turn.index = 1;
            Assert.IsFalse(room.IsRunning);
            room.Start();
            Assert.IsTrue(room.IsRunning);
            for (int i = 0; i < players.Length; i++)
            {
                var item = players[i];
                Assert.AreEqual("Start ", item.log);
                var chess = game.GetPlayerChess(item);
                Assert.AreEqual(i + 1, chess);
                item.Commit(new PlayRequest { y=i});
                Assert.AreEqual(chess, game.chessboard.GetValue(0, i));
            }
        }
        [Test]
        public void testGetPlayer()
        {
            var player = room.GetPlayer(0);
            Assert.AreEqual(null, player);
        }
        [Test]
        public void testFull()
        {
            room.Join(new Player(1));
            Assert.IsFalse(room.isFull());
            var p = new Player(2);
            room.Join(p);
            Assert.IsTrue(room.isFull());
            room.Remove(p);
            Assert.IsFalse(room.isFull());
        }
        [Test]
        public void testJoinPlayer()
        {
            var player0 = new Player(1);
            Assert.IsTrue(room.Join(player0));
            Assert.IsTrue(room.ContainPlayer(player0));
            Assert.AreEqual(room.Id, player0.RoomId);
            Assert.AreEqual(1, room.PlayerCount);
            Assert.AreEqual(player0, room.GetPlayer(player0.Id));
        }
        [Test]
        public void testJoinMaxPlayer()
        {
            for (int i = 0; i < 2; i++)
            {
                Assert.IsTrue(room.Join(new Player()));
            }
            Assert.AreEqual(room.maxPlayer, room.PlayerCount);
            Assert.IsFalse(room.Join(new Player()));
            Assert.AreEqual(room.maxPlayer, room.PlayerCount);
        }
        [Test]
        public void testRemovePlayer()
        {
            var player0 = new Player();
            room.Join(player0);
            room.Remove(player0);
            Assert.AreEqual(0, room.PlayerCount);
            Assert.AreEqual(0, player0.RoomId);
            Assert.IsFalse(room.ContainPlayer(player0));
        }
        [Test]
        public void testGameStartException()
        {
            var exp = Assert.Throws<GameException>(() =>
            {
                room.Start();
            });
            Assert.AreEqual("No enough player for start", exp.Message);
        }
        [Test]
        public void testPlayerOutLineOnStop()
        {
            var player0 = new Player();
            room.Join(player0);
            player0.OutLine();
            Assert.AreEqual(0, room.PlayerCount);
        }
        [Test]
        public void testPlayerOutLineOnRunning()
        {
            for (int c = 0; c < 2; c++)
            {
                var players = new Player[] 
                {
                    new Player(3),
                    new Player(4) 
                };
                for (int i = 0; i < players.Length; i++)
                {
                    room.Join(players[i]);
                }
                room.Start();
                players[0].OutLine();
                Assert.AreEqual(room.maxPlayer, room.PlayerCount);
                players[1].OutLine();
                Assert.AreEqual(0, room.PlayerCount);
                Assert.IsFalse(room.IsRunning);
            }
        }
    }
}
