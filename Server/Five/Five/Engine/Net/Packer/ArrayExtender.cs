using System;

namespace Five
{
    public class ArrayExtender<T>
    {
        public T[] TryExtend(T[] orgion,int count,int extendSize)
        {
            int cap = orgion.Length - count;
            if (cap < extendSize)
            {
                int len = GetExtendLength(orgion.Length, extendSize);
                T[] newArray = new T[len];
                Array.Copy(orgion, 0, newArray, 0, orgion.Length);
                return newArray;
            }
            return orgion;
        }
        int GetExtendLength(int len,int size)
        {
            int n = 1;
            int total = len + size;
            while (n < total)
                n <<= 1;
            return n;
        }
    }
}