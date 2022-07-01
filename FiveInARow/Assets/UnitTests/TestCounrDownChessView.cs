using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestCounrDownChessView
    {
        CounrDownChessView view;
        [SetUp]
        public void SetUp()
        {
            view = PrefabHelper.Instantiate<CounrDownChessView>("UI/CountDownView");
        }
        [Test]
        public void testChessUIView()
        {
            view.ChessType = 1;
            Assert.AreEqual(new Color(0, 0, 0, 1), view.bg.color);
            Assert.AreEqual(new Color(1, 1, 1, 1), view.text.color);
            view.ChessType = 2;
            Assert.AreEqual(new Color(1, 1, 1, 1), view.bg.color);
            Assert.AreEqual(new Color(0, 0, 0, 1), view.text.color);
        }
    }
}
