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
            var bgAlpha = view.bg.color.a;
            var txtAlpha = view.text.color.a;
            view.ChessType = 1;
            Assert.AreEqual(new Color(0, 0, 0, bgAlpha), view.bg.color);
            Assert.AreEqual(new Color(1, 1, 1, txtAlpha), view.text.color);
            view.ChessType = 2;
            Assert.AreEqual(new Color(1, 1, 1, bgAlpha), view.bg.color);
            Assert.AreEqual(new Color(0, 0, 0, txtAlpha), view.text.color);
        }
    }
}
