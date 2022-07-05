using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayMessage : Message
    {
        public int x;
        public int y;
        public virtual Message SetInfo(int x, int y)
        {
            this.x = x;
            this.y = y;
            return this;
        }
        public override string ToString()
        {
            return base.ToString() + $" ({x},{y})";
        }
    }
}
