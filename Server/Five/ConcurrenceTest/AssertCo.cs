using FivesUnitTest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConcurrenceTest
{
    public static class AssertCo
    {
        public static void AssertSequence(int[] array)
        {
            AssertSequence(array, (i) => i);
        }

        internal static void AssertSequence<T>(T[] array, Func<T, int> property)
        {
            Array.Sort(array, (a, b) =>
            {
                return property(a) - property(b);
            });
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(i + 1, property(array[i]));
            }
        }
    }
}
