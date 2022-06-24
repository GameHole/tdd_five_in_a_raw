using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Proto
    {
        public int proto = 0x78787878;
        public void Write(ByteStream stream)
        {
            stream.Write(proto);
        }
        public bool IsVailed(ByteStream stream)
        {
            return stream.Read<int>() == proto;
        }
    }
}
