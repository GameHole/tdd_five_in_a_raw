using System;

namespace Five
{
    public class OpCodeErrorResponseProcesser : IProcesser
    {
        public int OpCode => throw new NotImplementedException();

        public void Process(ASocket socket, Message message)
        {
            socket.Send(new OpCodeErrorMessage(message.opcode));
        }
    }
}
