namespace Five
{
    public class MatchResponse : Response
    {
        public int playerId;

        public virtual Response SetInfo(Message message, Result result,int playerId)
        {
            this.playerId = playerId;
            return base.SetInfo(message,result);
        }
        public override string ToString()
        {
            return base.ToString()+$" id:{playerId}";
        }
    }
}
