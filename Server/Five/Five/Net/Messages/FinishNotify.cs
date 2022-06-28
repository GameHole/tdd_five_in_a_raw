namespace Five
{
    public class FinishNotify : Message
    {
        public int id { get; private set; }
        public FinishNotify(int id):base(MessageCode.FinishNotify)
        {
            this.id = id;
        }
        public override string ToString()
        {
            return base.ToString() + " " + id;
        }
    }
}