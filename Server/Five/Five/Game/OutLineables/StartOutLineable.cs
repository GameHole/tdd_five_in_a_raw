using System.Threading;

namespace Five
{
    internal class StartOutLineable : IOutLineable
    {
        private Room game;

        public StartOutLineable(Room game)
        {
            this.game = game;
        }
        int outlinePlayerCount;
        public void OutLine()
        {
            Interlocked.Increment(ref outlinePlayerCount);
            if (outlinePlayerCount == game.maxPlayer)
            {
                game.Stop();
                Interlocked.Exchange(ref outlinePlayerCount, 0);
            }
        }
    }
}