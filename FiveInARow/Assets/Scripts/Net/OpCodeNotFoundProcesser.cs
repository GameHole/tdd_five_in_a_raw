using UnityEngine;

namespace Five
{
    public class OpCodeNotFoundProcesser : IProcesser
    {
        public int OpCode => throw new System.NotImplementedException();
        public ILogger logger = Debug.unityLogger;
        public void Process(Message message)
        {
            logger.LogWarning("",$"OpCode:{message.opcode} not process");
        }
    }
}