using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnitTests
{
    class LearnTest
    {
        [Test]
        public void testVector3Equal()
        {
            Assert.AreEqual(new Vector3(1, 2, 3), new Vector3(1, 2, 3));
        }
        [Test]
        public void testVector2IntHashCode()
        {
            Assert.AreNotEqual(new Vector2Int(1, 3).GetHashCode(), new Vector2Int(1, 2).GetHashCode());
            Assert.AreEqual(new Vector2Int(1, 2).GetHashCode(), new Vector2Int(1, 2).GetHashCode());
        }
        [UnityTest]
        public IEnumerator testIsDestroy()
        {
            var go = new GameObject();
            Object.Destroy(go);
            Assert.IsTrue(go);
            yield return null;
            Assert.IsFalse(go);
        }
        [Test]
        public void testIsDestroyImmediate()
        {
            var go = new GameObject();
            Object.DestroyImmediate(go);
            Assert.IsFalse(go);
        }
        [Test]
        public void testColorEqual()
        {
            Assert.AreEqual(new Color(0.1f, 0.1f, 0.1f, 0.1f), new Color(0.1f, 0.1f, 0.1f, 0.1f));
        }
    }
}
