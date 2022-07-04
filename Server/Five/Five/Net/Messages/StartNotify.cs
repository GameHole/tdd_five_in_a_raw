namespace Five
{
    public class StartNotify : Message
    {
        public PlayerInfo[] infos;
        public StartNotify()
        {
            opcode = MessageCode.StartNotify;
        }
        public override string ToString()
        {
            var info = "";
            foreach (var item in infos)
            {
                info += $"({item.Chess},{item.PlayerId})";
            }
            return base.ToString() + " " + info;
        }
    }
}
