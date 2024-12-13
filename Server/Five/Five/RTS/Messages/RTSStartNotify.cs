using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSStartNotify : Message
    {
        public IEnumerable<RTSCharater> info;
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
                str += $"({item.Id},{item.x},{item.y})";
            }
            str += $"({fshx},{fshy})";
            return base.ToString() + str;
        }
    }
}
