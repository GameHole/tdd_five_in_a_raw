using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSStartNotify : Message
    {
        public IEnumerable<Charater> info;
        public float fshx;
        public float fshy;

        public RTSStartNotify()
        {
            opcode = MessageCode.StartNotify;
        }
        public override string ToString()
        {
            string str = default;
            foreach (var item in info)
            {
                str += $"({item.id},{item.x},{item.y})";
            }
            str += $"({fshx},{fshy})";
            return base.ToString() + str;
        }
    }
}
