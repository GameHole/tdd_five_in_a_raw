using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class MatcherMgr
    {
        public ConcurrentDictionary<ASocket, Matcher> matchers { get; }
        public MatcherMgr()
        {
            matchers = new ConcurrentDictionary<ASocket, Matcher>();
        }
        public void Login(ASocket socket)
        {
            Matcher matcher = NewMatcher(socket);
            matcher.Player.notifier = new NetNotifier(socket, matcher.Player);
            matchers.TryAdd(socket, matcher);
        }
        private Matcher NewMatcher(ASocket socket)
        {
            var matcher = new Matcher();
            socket.onClose += () =>
            {
                matcher.Player.OutLine();
                matchers.TryRemove(socket, out var c);
            };
            return matcher;
        }

        public void Stop()
        {
            matchers.Clear();
        }
        public Result Play(int x, int y, ASocket sok)
        {
            return matchers[sok].Player.Play(x, y);
        }
    }
}
