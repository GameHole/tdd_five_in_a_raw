using NUnit.Framework;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

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
        [Test]
        public void testAsync()
        {
            var task = AsyncFunc().GetAwaiter();
            Assert.AreEqual(1, task.GetResult());
        }
        Task<int> AsyncFunc()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10);
                return 1;
            });
        }
        [Test]
        public void testButtonListenerCount()
        {
            var btn = new GameObject().AddComponent<Button>();
            btn.onClick.AddListener(() => { });
            Assert.AreEqual(0, btn.onClick.GetPersistentEventCount());
        }
    }
}
