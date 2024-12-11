namespace Five
{
    public abstract class AGame: IPlayable
    {
        protected IRoom room;
        public virtual void Init(IRoom room)
        {
            this.room = room;
        }
        public abstract void Start();
        public abstract void Stop();
        public abstract Result Commit(Message message, Player player);
    }
}
