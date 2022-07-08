using UnityEngine;

namespace Five
{
    public class ResponseDecorater : AProcesser<Response>
    {
        public IProcesser decorated { get; }
        public ILogger logger;
        public ResponseDecorater(IProcesser decorated)
        {
            this.decorated = decorated;
            logger = Debug.unityLogger;
        }

        public override int OpCode => decorated.OpCode;

        public override void ProcessContent(Response message)
        {
            if (message.result == 0)
            {
                decorated.Process(message);
            }
            else
            {
                logger.LogError("",$"Response error,result:{message.result}");
            }
        }
    }
}
