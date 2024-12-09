namespace Five
{
    public abstract class AGame
    {
        protected IRoom room;
        public virtual void Init(IRoom room)
        {
            this.room = room;
        }

        public abstract void Start();
        public abstract void Stop();
    }
}
