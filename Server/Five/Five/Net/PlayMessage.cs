using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayMessage : Message
    {
        public int x;
        public int y;
        public PlayMessage(int x,int y) : base(3)
        {
            this.x = x;
            this.y = y;
        }
    }
}
