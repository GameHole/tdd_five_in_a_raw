using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class MessagePacker
    {
        private Proto proto;

        public MessagePacker(Proto proto, int cap = 12)
        {
            this.proto = proto;
            stream = new ByteStream(cap);
        }

        public ByteStream stream { get; private set; }

        public void Pack(ByteStream byteStream)
        {
            PackInternal(stream, byteStream);
        }
        private void PackInternal(ByteStream stream,ByteStream msgStream)
        {
            proto.Write(stream);
            stream.Write(msgStream.Count);
            stream.Write(msgStream);
        }
        bool ContainProto()
        {
            for (; stream.Index < stream.Count; stream.Index++)
            {
                if (proto.IsVailed(stream))
                    return true;
            }
            return false;
        }
        bool isIntact(out int length)
        {
            length = 0;
            if(stream.GetLastCount() > sizeof(int))
            {
                length = stream.Read<int>();
                if (length <= stream.GetLastCount())
                    return true;
            }
            return false ;
        }
        public bool Unpack(out ByteStream outStream)
        {
            outStream = default;
            var orgionIndex = stream.Index;
            if (ContainProto())
            {
                stream.Index += proto.ByteSize;
                if (isIntact(out int len))
                {
                    outStream = new ByteStream(stream.Bytes, stream.Index, stream.Index + len);
                    stream.Index += len;
                    return true;
                }
            }
            stream.Index = orgionIndex;
            return false;
        }

        internal ByteStream PackNew(ByteStream msgStream)
        {
            var retstram = new ByteStream();
            PackInternal(retstram, msgStream);
            return retstram;
        }
        public void MoveBrokenBytesToHead()
        {
            int last = stream.GetLastCount();
            Array.Copy(stream.Bytes, stream.Index, stream.Bytes, 0, last);
            stream.Index = 0;
            stream.Count = last;
        }
    }
}
