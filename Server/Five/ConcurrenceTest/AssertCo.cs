using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConcurrenceTest
{
    public static class AssertCo
    {
        public static void AssertAllNotEqual(int[] array)
        {
            Array.Sort(array, (a, b) =>
            {
                return a - b;
            });
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(i + 1, array[i]);
            }
        }
    }
}
