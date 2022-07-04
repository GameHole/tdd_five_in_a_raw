namespace Five
{
    public class PlayerIdNotify:Message
    {
        public int playerId;
        public PlayerIdNotify() { }
        public PlayerIdNotify(int opcode,int playerId):base(opcode)
        {
            this.playerId = playerId;
        }
        public override string ToString()
        {
            return base.ToString() + " " + playerId;
        }
    }
}
