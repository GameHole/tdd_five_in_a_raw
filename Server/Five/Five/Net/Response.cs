using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Response:Message
    {
        public int result;

        public Response(Message message,Result result):base(message.opcode)
        {
            this.result = result.code;
        }
        public override string ToString()
        {
            return $"{base.ToString()} result:{result}";
        }
    }
}
