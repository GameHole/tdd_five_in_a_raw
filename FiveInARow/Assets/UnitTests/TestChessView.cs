using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestChessView
    {
        [Test]
        public void testChessView()
        {
            var view = PrefabHelper.Instantiate<ChessView>("GameObjects/Chess");
            view.ChessType = 1;
            Assert.AreEqual(new Color(0, 0, 0, 1), view.color);
            Assert.AreEqual(view.color, view.GetComponentInChildren<MeshRenderer>().material.color);
            view.ChessType = 2;
            Assert.AreEqual(new Color(1, 1, 1, 1), view.color);
        }
    }
}
