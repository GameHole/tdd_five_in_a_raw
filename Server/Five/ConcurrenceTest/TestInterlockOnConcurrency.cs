using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrenceTest
{
    internal class TestInterlockOnConcurrency
    {
        class T
        {
            public int index;

            internal void add()
            {
                Interlocked.Increment(ref index);
            }

            internal int add1()
            {
                return Interlocked.Increment(ref index);
            }

            internal virtual int viradd()
            {
                return Interlocked.Increment(ref index);
            }
            int pIndex;
            internal int Pindex => pIndex;

            internal int priadd()
            {
                return Interlocked.Increment(ref pIndex);
            }
        }
        int count = 100000;
        int index;
        int[] array;
        [SetUp]
        public void set()
        {
            array = new int[count];
        }
        [Test]
        public async Task testArrayForClassAttribute()
        {
            T t = new T();
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = Interlocked.Increment(ref t.index);
            });
            Assert.AreEqual(count, t.index);
            AssertCo.AssertAllNotEqual(array);
        }
        

        [Test]
        public async Task testArrayForClassSelf()
        {
            index = 0;
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = Interlocked.Increment(ref this.index);
            });
            Assert.AreEqual(count, index);
            AssertCo.AssertAllNotEqual(array);
        }

        [Test]
        public void testTadd()
        {
            T t = new T();
            t.add();
            Assert.AreEqual(1, t.index);
        }
        [Test]
        public async Task testArrayForClassMethord()
        {
            T t = new T();
            await Repeat.RepeatAsync(count, (index) =>
            {
                t.add();
                array[index] = t.index;
            });
            Assert.AreEqual(count, t.index);
            Assert.Throws<AssertionException>(() =>
            {
                AssertCo.AssertAllNotEqual(array);
            });
        }
        [Test]
        public async Task testArrayForClassMethord1()
        {
            T t = new T();
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = t.add1();
            });
            Assert.AreEqual(count, t.index);
            AssertCo.AssertAllNotEqual(array);
        }
        [Test]
        public async Task testArrayForClassVirMethord()
        {
            T t = new T();
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = t.viradd();
            });
            Assert.AreEqual(count, t.index);
            AssertCo.AssertAllNotEqual(array);
        }
        [Test]
        public async Task testArrayForClassPrivateAttr()
        {
            T t = new T();
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = t.priadd();
            });
            Assert.AreEqual(count, t.Pindex);
            AssertCo.AssertAllNotEqual(array);
        }
        [Test]
        public async Task testArrayForInner()
        {
            int mark = 0;
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = Interlocked.Increment(ref mark);
            });
            Assert.AreEqual(count, mark);
            AssertCo.AssertAllNotEqual(array);
        }
        [Test]
        public async Task testArrayForLock()
        {
            int mark = 0;
            await Repeat.RepeatAsync(count, (index) =>
            {
                lock (array)
                    array[index] = ++mark;
            });
            Assert.AreEqual(count, mark);
            AssertCo.AssertAllNotEqual(array);
        }
        [Test]
        public async Task testArrayForLocal()
        {
            await Repeat.RepeatAsync(count, (index) =>
            {
                array[index] = index;
            });
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(i, array[i]);
            }
        }
    }
}
