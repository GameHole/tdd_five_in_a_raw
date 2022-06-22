using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestTurn
    {
        [Test]
       public void testTurn()
        {
            var turn = new Turn(2);
            turn.Start();
            Assert.AreEqual(0, turn.index);
            turn.Next();
            Assert.AreEqual(1, turn.index);
            turn.Next();
            Assert.AreEqual(0, turn.index);
        }
        [Test]
        public void testTurnStart()
        {
            var turn = new Turn(2);
            turn.Next();
            turn.Start();
            Assert.AreEqual(0, turn.index);
        }
        [Test]
        public void testTurnEvent()
        {
            var turn = new Turn(2);
            var log = "";
            turn.onTurn += (index) =>
            {
                log += $"eve:{index} ";
            };
            turn.Start();
            Assert.AreEqual("eve:0 ", log);
            turn.Next();
            Assert.AreEqual("eve:0 eve:1 ", log);
            turn.Next();
            Assert.AreEqual("eve:0 eve:1 eve:0 ", log);
        }
    }
}
