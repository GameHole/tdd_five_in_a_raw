using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestChessSelector
    {
        ChessSelectorView selector;
        PositionConvertor convertor;
        [SetUp]
        public void SetUp()
        {
            convertor = new PositionConvertor();
            selector = new ChessSelectorView(new TestRay(), new BoardRaycastor(15, 15));
        }
        [Test]
        public void testChessSelector()
        {
            Assert.IsFalse(selector.IsRun);
            Assert.IsFalse(selector.ChessPreview.activeInHierarchy);
            Assert.NotNull(selector.ChessPreview.GetComponent<ChessView>());
        }
        [Test]
        public void testUpdate()
        {
            selector.Start();
            selector.Update();
            Assert.IsTrue(selector.ChessPreview.activeInHierarchy);
            Assert.AreEqual(convertor.ToLocalPosition(selector.SelectedPosition), selector.ChessPreview.transform.position);
        }
        [Test]
        public void testPlace()
        {
            selector.Start();
            Vector2Int pos = new Vector2Int();
            selector.onPlace += (p) => pos = p;
            selector.Place();
            Assert.AreEqual(pos, selector.SelectedPosition);
        }
        [Test]
        public void testStop()
        {
            selector.Start();
            selector.Stop();
            Assert.IsFalse(selector.ChessPreview.activeInHierarchy);
            Assert.IsFalse(selector.IsRun);
            selector.Update();
            Assert.IsFalse(selector.ChessPreview.activeInHierarchy);
            var log = "";
            selector.onPlace += (p) => log = "run";
            selector.Place();
            Assert.AreEqual("", log);
        }
        [Test]
        public void testStart()
        {
            selector.Start();
            Assert.IsTrue(selector.IsRun);
        }
    }
}
