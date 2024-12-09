using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningTest
{
    internal class TestSort
    {
        [Test]
        public void test()
        {
            int[] array = new int[100000];
            for (int i = 100 - 1; i >= 0; i--)
            {
                array[i] = i + 1;
            }
            Assert.Throws<InvalidOperationException>(() =>
            {
                Array.Sort(array, (a, b) =>
                {
                    if (a == b)
                        throw new Exception();
                    return a - b;
                });
            });
        }
    }
}
