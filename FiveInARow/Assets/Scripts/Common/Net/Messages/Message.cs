using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Message
    {
        public int opcode;

        public Message(int opcode)
        {
            this.opcode = opcode;
        }
        public override string ToString()
        {
            return $"opcode:{opcode}";
        }
    }
}
