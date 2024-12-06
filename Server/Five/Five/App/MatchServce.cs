using System;

namespace Five
{
    public class MatchServce
    {
        private MatcherMgr mgr;
        private GameMgr gameRsp;
        private App app;
        private GameFactroy factroy;

        public MatchServce(App app,GameFactroy factroy)
        {
            this.mgr = app.mgr;
            this.gameRsp = app.gameRsp;
            this.app = app;
            this.factroy = factroy;
        }

        public Result Cancel(AClient socket)
        {
            var players = mgr.matchers[socket];
            lock (gameRsp.lcoker)
            {
                var game = gameRsp.GetRoom(players.RoomId);
                if (game == null)
                {
                    return ResultDefine.NotInMatching;
                }
                if (game.IsRunning)
                {
                    return ResultDefine.GameStarted;
                }
                game.Leave(players);
                return ResultDefine.Success;
            }
        }

        public Result Match(AClient socket)
        {
            var player = mgr.matchers[socket];
            lock (gameRsp.lcoker)
            {
                var game = gameRsp.GetRoom(player.RoomId);
                if (game == null)
                {
                    game = FindNotStartGame();
                    game.Join(player);
                    player.Match();
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
        private Room FindNotStartGame()
        {
            foreach (var item in gameRsp)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            var game = factroy.Factroy();
            return gameRsp.NewRoom(game);
        }
        public Result Play(int x, int y, AClient sok)
        {
            return mgr.matchers[sok].Play(x, y);
        }

        public void Stop()
        {
            app.Stop();
        }

        public void Login(AClient socket)
        {
            app.Login(socket);
        }
    }
}
