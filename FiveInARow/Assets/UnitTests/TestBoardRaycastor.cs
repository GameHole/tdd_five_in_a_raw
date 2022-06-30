using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestBoardRaycastor
    {
        float width = 15;
        float height = 16;
        BoardRaycastor castor;
        [SetUp]
        public void SetUp()
        {
            castor = new BoardRaycastor(width, height);
        }
        [Test]
        public void testChessSelector()
        {
            Assert.NotNull(castor.colider);
            var tran = castor.colider.transform;
            var scale = tran.localScale;
            var w = width * 0.5f;
            var h = height * 0.5f;
            Assert.Greater(scale.x,w);
            Assert.Greater(scale.z, h);
            Assert.LessOrEqual(scale.y, 0.001f);
            Assert.AreEqual(new Vector3(w, 0, h), tran.position);
        }
        [Test]
        public void testRaycast()
        {
            Assert.IsFalse(castor.Raycast(new Ray(new Vector3(-1,1,-1),Vector3.down),out Vector2Int pos));
            Assert.IsTrue(castor.Raycast(new Ray(new Vector3(0.5f, 1, 0.5f), Vector3.down), out pos));
            Assert.AreEqual(new Vector2Int(1, 1), pos);
        }
    }
}
