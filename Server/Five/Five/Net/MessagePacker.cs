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
            recevingStream = new ByteStream(cap);
        }

        public ByteStream recevingStream { get; private set; }

        public void Pack(ByteStream byteStream)
        {
            PackInternal(recevingStream, byteStream);
        }
        private void PackInternal(ByteStream stream,ByteStream msgStream)
        {
            proto.Write(stream);
            stream.Write(msgStream.Count);
            stream.Write(msgStream);
        }
        bool ContainProto()
        {
            for (; recevingStream.Index < recevingStream.Count; recevingStream.Index++)
            {
                if (proto.IsVailed(recevingStream))
                    return true;
            }
            return false;
        }
        bool isIntact(out int length)
        {
            length = 0;
            if(recevingStream.GetLastCount() > sizeof(int))
            {
                length = recevingStream.Read<int>();
                if (length <= recevingStream.GetLastCount())
                    return true;
            }
            return false ;
        }
        public bool Unpack(out ByteStream outStream)
        {
            outStream = default;
            var orgionIndex = recevingStream.Index;
            if (ContainProto())
            {
                recevingStream.Index += proto.ByteSize;
                if (isIntact(out int len))
                {
                    outStream = new ByteStream(recevingStream.Bytes, recevingStream.Index, recevingStream.Index + len);
                    recevingStream.Index += len;
                    return true;
                }
            }
            recevingStream.Index = orgionIndex;
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
            int last = recevingStream.GetLastCount();
            Array.Copy(recevingStream.Bytes, recevingStream.Index, recevingStream.Bytes, 0, last);
            recevingStream.Index = 0;
            recevingStream.Count = last;
        }
    }
}
