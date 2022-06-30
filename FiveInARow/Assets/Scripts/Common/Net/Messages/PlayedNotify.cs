namespace Five
{
    public class PlayedNotify : PlayMessage
    {
        public int id;
        public PlayedNotify(int x, int y,int id) :base(MessageCode.PlayedNotify,x,y)
        {
            this.id = id;
        }
        public override string ToString()
        {
            return base.ToString()+$" ({x},{y}){id}";
        }
    }
}