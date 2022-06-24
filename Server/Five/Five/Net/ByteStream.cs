using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ByteStream
    {
        static readonly byte[] empty = new byte[0];
        byte[] bytes = empty;
        public int Index { get; private set; }
        public int Count { get; private set; }
        private ArrayExtender<byte> extender = new ArrayExtender<byte>();
        public unsafe T Read<T>()where T:unmanaged
        {
            T value = Peek<T>();
            Index += sizeof(T);
            return value;
        }
        public unsafe T Peek<T>() where T : unmanaged
        {
            int size = sizeof(T);
            int last = Count - Index;
            if (size > last)
                throw new StreamException($"No enought length for reading type. Need {size},but last {last}");
            T value = default;
            fixed (byte* bytesPtr = bytes)
            {
                value = *(T*)(bytesPtr + Index);
            }
            return value;
        }
        public unsafe void Write<T>(T value) where T : unmanaged
        {
            int size = sizeof(T);
            bytes = extender.TryExtend(bytes, Count, size);
            int index = Count;
            fixed(byte* bytesPtr = bytes)
            {
                *(T*)(bytesPtr + index) = value;
            }
            Count += size;
        }
        public void ResetIndex()
        {
            Index = 0;
        }
    }
}
