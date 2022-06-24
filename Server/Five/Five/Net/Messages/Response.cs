using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Response:Message
    {
        public int result;
        public Response(int opcode, int result) : base(opcode)
        {
            this.result = result;
        }
        public Response(Message message,Result result):this(MessageCode.GetResponseCode(message.opcode), result.code)
        {
        }
        public override string ToString()
        {
            return $"{base.ToString()} result:{result}";
        }
    }
}
