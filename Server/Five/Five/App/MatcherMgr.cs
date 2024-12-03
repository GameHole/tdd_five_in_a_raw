using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class MatcherMgr
    {
        public GameMgr gameRsp { get; }
        public ConcurrentDictionary<ASocket, Matcher> matchers { get; }

        public MatcherMgr(GameMgr matching)
        {
            this.gameRsp = matching;
            matchers = new ConcurrentDictionary<ASocket, Matcher>();
        }
        public void Invoke(ASocket socket)
        {
            Matcher matcher = NewMatcher(socket);
            matchers.TryAdd(socket, matcher);
        }

        private Matcher NewMatcher(ASocket socket)
        {
            var matcher = new Matcher();
            matcher.Player.notifier = new NetNotifier(socket, matcher.Player);
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

        public Result Match(ASocket socket)
        {
            var matcher = matchers[socket];
            lock (gameRsp.lcoker)
            {
                var game = gameRsp.GetGame(matcher.GameId);
                if (game == null)
                {
                    game = FindNotStartGame();
                    game.Join(matcher.Player);
                    matcher.Matched();
                    if (game.isFull())
                    {
                        game.Start();
                    }
                    return ResultDefine.Success;
                }
                if (game.IsRunning)
                {
                    return ResultDefine.GameStarted;
                }
                return ResultDefine.Matching;
            }
        }
        private Game FindNotStartGame()
        {
            foreach (var item in gameRsp)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return gameRsp.NewGame();
        }
        public Result Cancel(ASocket socket)
        {
            var matcher = matchers[socket];
            lock (gameRsp.lcoker)
            {
                var game = gameRsp.GetGame(matcher.GameId);
                if (game == null)
                {
                    return ResultDefine.NotInMatching;
                }
                if (game.IsRunning)
                {
                    return ResultDefine.GameStarted;
                }
                game.Remove(matcher.Player);
                matcher.Canceled();
                return ResultDefine.Success;
            }
        }
        public Result Play(int x, int y, ASocket sok)
        {
            return matchers[sok].Player.Play(x, y);
        }
    }
}
