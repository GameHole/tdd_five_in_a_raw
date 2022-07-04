

namespace Five
{
    public abstract class AProcesser<T> : IProcesser where T:Message
    {
        public abstract int OpCode { get; }

        public void Process(Message message)
        {
            ProcessContent(message as T);
        }
        public abstract void ProcessContent(T message);
    }
}
