using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class MatcherMgr
    {
        public ConcurrentDictionary<AClient, Player> matchers { get; }
        public MatcherMgr()
        {
            matchers = new ConcurrentDictionary<AClient, Player>();
        }
        public void Login(AClient socket)
        {
            Player player = NewMatcher(socket);
            player.notifier = new NetNotifier(socket, player);
            matchers.TryAdd(socket, player);
        }
        private Player NewMatcher(AClient socket)
        {
            var player = new Player();
            socket.onClose += () =>
            {
                player.OutLine();
                matchers.TryRemove(socket, out var c);
            };
            return player;
        }

        public void Stop()
        {
            matchers.Clear();
        }
        public Result Play(int x, int y, AClient sok)
        {
            return matchers[sok].Play(x, y);
        }
    }
}
