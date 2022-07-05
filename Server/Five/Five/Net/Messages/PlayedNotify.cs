namespace Five
{
    public class PlayedNotify : PlayMessage
    {
        public int id;
        public PlayedNotify()
        {
            opcode = MessageCode.PlayedNotify;
        }
        public override string ToString()
        {
            return base.ToString()+$"{id}";
        }
    }
}