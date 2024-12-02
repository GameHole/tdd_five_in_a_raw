using System;

namespace Five
{
    public class OpCodeErrorResponseProcesser : IProcesser
    {
        public int OpCode => throw new NotImplementedException();

        private ASocket socket;

        public OpCodeErrorResponseProcesser(ASocket socket)
        {
            this.socket = socket;
        }

        public void Process(ASocket socket, Message message)
        {
            socket.Send(new OpCodeErrorMessage(message.opcode));
        }
    }
}
