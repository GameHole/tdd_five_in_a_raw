namespace Five
{
    public abstract class AGame: IPlayable
    {
        protected IRoom room;
        public virtual void Init(IRoom room)
        {
            this.room = room;
        }
        public virtual void Start()
        {
            SetPlayable();
        }
        private void SetPlayable()
        {
            foreach (var item in room.Players)
            {
                item.playable = this;
            }
        }
        public abstract void Stop();
        public abstract Result Commit(Message message, Player player);
    }
}
