using System.Threading;

namespace Five
{
    internal class StartOutLineable : IOutLineable
    {
        private Room room;

        public StartOutLineable(Room room)
        {
            this.room = room;
        }
        int outlinePlayerCount;
        public void OutLine()
        {
            Interlocked.Increment(ref outlinePlayerCount);
            if (outlinePlayerCount == room.maxPlayer)
            {
                room.Stop();
                Interlocked.Exchange(ref outlinePlayerCount, 0);
            }
        }
    }
}