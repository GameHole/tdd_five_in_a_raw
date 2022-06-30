using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayMessage:Message
    {
        public int x;
        public int y;
        public PlayMessage(int opcode,int x, int y):base(opcode)
        {
            this.x = x;
            this.y = y;
        }
    }
}
