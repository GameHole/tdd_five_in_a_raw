using Five;
using NUnit.Framework;

namespace UnitTests
{
    class TestContainer
    {
        Container container;
        [SetUp]
        public void SetUp()
        {
            container = new Container();
        }
        [Test]
        public void testContainer()
        {
            Assert.AreEqual(0, container.Count);
        }
        [Test]
        public void testGet()
        {
            Assert.IsNull(container.Get<object>());
        }
        [Test]
        public void testContain()
        {
            Assert.IsFalse(container.Contain(typeof(object)));
            container.Set(new object());
            Assert.IsTrue(container.Contain(typeof(object)));
        }
        [Test]
        public void testSet()
        {
            var old = new object();
            Assert.AreSame(old, container.Set(old));
            Assert.AreEqual(1, container.Count);
            Assert.NotNull(container.Get<object>());
            var obj = new object();
            container.Set(obj);
            Assert.AreEqual(1, container.Count);
            Assert.AreSame(obj,container.Get<object>());
        }
    }
}
