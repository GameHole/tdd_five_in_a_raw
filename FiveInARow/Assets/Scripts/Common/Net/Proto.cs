using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Proto
    {
        public readonly int proto = 0x78787878;

        public int ByteSize { get => sizeof(int); }

        public void Write(ByteStream stream)
        {
            stream.Write(proto);
        }
        public bool IsVailed(ByteStream stream)
        {
            Console.WriteLine(stream.GetLastCount());
            return stream.GetLastCount() >= ByteSize && stream.Peek<int>() == proto;
        }
    }
}
