using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    internal class TestPlayServce
    {
        private TGameFactroy fact;
        private Domain domain;
        private LogPlayer logPlayer;
        private LogPlayer logPlayer1;

        [SetUp]
        public void set()
        {
            fact = new TGameFactroy();
            domain = new Domain(fact, new TIdGenrator());
            logPlayer = LogPlayer.EmntyLog();
            logPlayer1 = LogPlayer.EmntyLog(1);
        }
        [Test]
        public void testPlayServce()
        {
            domain.playerRsp.Add(logPlayer);
            domain.playerRsp.Add(logPlayer1);
            domain.matchServce.Match(0);
            domain.matchServce.Match(1);
            var message = new PlayRequest { x = 1, y = 2 };
            var result = domain.playServce.Commit(0, message);
            Assert.AreSame(message, fact.game.msg);
            Assert.AreEqual(-1, result.code);
        }
        [Test]
        public void testPlayOnLogin()
        {
            domain.playerRsp.Add(logPlayer);
            var message = new PlayRequest { x = 1, y = 2 };
            var result = domain.playServce.Commit(0, message);
            Assert.IsNull(fact.game.msg);
            Assert.AreEqual(ResultDefine.PlayerNotInTheGame, result);
        }
        [Test]
        public void testPlayOnNewGame()
        {
            domain.playerRsp.Add(logPlayer);
            domain.matchServce.Match(0);
            var message = new PlayRequest { x = 1, y = 2 };
            var result = domain.playServce.Commit(0, message);
            Assert.IsNull(fact.game.msg);
            Assert.AreEqual(ResultDefine.GameNotStart, result);
        }
    }
}
