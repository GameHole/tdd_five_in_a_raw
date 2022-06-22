using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class GameFlowTest
    {
        Game game;
        LogPlayer player0;
        LogPlayer player1;
        [SetUp]
        public void SetUp()
        {
            game = new Game();
            player0 = new LogPlayer();
            player1 = new LogPlayer();
            game.Join(player0);
            game.Join(player1);
            game.Start();
        }
        [Test]
        public void testGameStart()
        {
            var localGame = new Game();
            player0 = new LogPlayer();
            player1 = new LogPlayer();
            localGame.Join(player0);
            localGame.Join(player1);
            localGame.turn.index = 1;
            localGame.Start();
            Assert.AreEqual("Start ", player0.log);
            Assert.AreEqual("Start ", player1.log);
            Assert.AreEqual(1, player0.chess);
            Assert.AreEqual(2, player1.chess);
            Assert.AreEqual(0, localGame.turn.index);
            player0.Play(0, 0);
            Assert.AreEqual(player0.chess, localGame.chessboard.GetValue(0, 0));
            player1.Play(0, 1);
            Assert.AreEqual(player1.chess, localGame.chessboard.GetValue(0, 1));
        }
        [Test]
        public void testGameStartRepeet()
        {
            Assert.AreEqual(0, game.turn.index);
            game.Start();
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
            player0.Play(0, 0);
            Assert.AreEqual(0, game.chessboard.GetValue(0, 0));
            Assert.AreEqual(0, game.chessboard.Count);
            player1.Play(0, 0);
            Assert.AreEqual(0, game.turn.index);
            Assert.AreEqual(1, game.chessboard.Count);
        }
        [Test]
        public async Task testFinish()
        {
            game.timer.time = 1;
            var log = "";
            game.onFinish += (id) =>
            {
                log += $"onFinish:{id}";
            };
            game.chessboard.AddValue(0,0, player0.chess);
            game.chessboard.AddValue(1, 0, player0.chess);
            game.chessboard.AddValue(2, 0, player0.chess);
            game.chessboard.AddValue(3, 0, player0.chess);
            player0.Play(4, 0);
            Assert.AreEqual("onFinish:0", log);
            Assert.AreEqual(0, game.turn.index);
            player1.Play(10, 10);
            Assert.AreEqual("onFinish:0", log);
            Assert.AreEqual(0, game.turn.index);
            Assert.AreEqual(0, game.chessboard.GetValue(10,10));
            await Task.Delay(1100);
            Assert.AreEqual(0, game.turn.index);
        }
        [Test]
        public void testPlay()
        {
            var result = player0.Play(1, 0);
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual(1, game.chessboard.GetValue(1, 0));
            Assert.AreEqual(1, game.turn.index);
            result = player0.Play(2, 0);
            Assert.AreEqual(ResultDefine.NotCurrentTurnPlayer, result);
            Assert.AreEqual(0, game.chessboard.GetValue(2, 0));
            Assert.AreEqual(1, game.turn.index);
        }
        [Test]
        public void testPlayOnePlace()
        {
            game.chessboard.AddValue(1, 0, 2);
            var result = player0.Play(1, 0);
            Assert.AreEqual(ResultDefine.AllReadyHasChess, result);
            Assert.AreEqual(2, game.chessboard.GetValue(1, 0));
            Assert.AreEqual(0, game.turn.index);
        }
    }
}
