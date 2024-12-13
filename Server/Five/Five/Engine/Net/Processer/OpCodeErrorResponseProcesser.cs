using System;

namespace Five
{
    public class OpCodeErrorResponseProcesser : IProcesser
    {
        public void Process(AClient socket, Message message)
        {
            socket.Send(new OpCodeErrorMessage(message.opcode));
        }
    }
}
