using Five;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
namespace ConcurrenceTest
{
    public class TestNetList
    {
        const int count = 1000;
        ConcurrentList<object> list;
        [SetUp]
        public void SetUp()
        {
            list = new ConcurrentList<object>();
        }
        [Test]
        public async Task testAdd()
        {
            await Repeat.RepeatAsync(count, (i) =>
            {
                list.Add(new object());
            });
            Assert.AreEqual(count, list.Count);
        }

        [Test]
        public async Task testRemove()
        {
            List<object> objs = new List<object>();
            list.onAdd += (c) => objs.Add(c);
            for (int i = 0; i < count; i++)
            {
                list.Add(new object());
            }
            Assert.AreEqual(count, list.Count);
            await Repeat.RepeatAsync(objs, (obj) =>
            {
                list.Remove(obj);
            });
            Assert.AreEqual(0, list.Count);
        }
        [Test]
        public async Task testAddRemove()
        {
            list.onAdd += (c) => list.Remove(c);
            await Repeat.RepeatAsync(count, (i) =>
            {
                list.Add(new object());
            });
            Assert.AreEqual(0, list.Count);
        }
    }
}