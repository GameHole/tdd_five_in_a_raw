using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class MatcherMgr
    {
        public ConcurrentDictionary<ASocket, Player> matchers { get; }
        public MatcherMgr()
        {
            matchers = new ConcurrentDictionary<ASocket, Player>();
        }
        public void Login(ASocket socket)
        {
            Player player = NewMatcher(socket);
            player.notifier = new NetNotifier(socket, player);
            matchers.TryAdd(socket, player);
        }
        private Player NewMatcher(ASocket socket)
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
        public Result Play(int x, int y, ASocket sok)
        {
            return matchers[sok].Play(x, y);
        }
    }
}
