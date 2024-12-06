using System;

namespace Five
{
    public class MatchServce
    {
        private App app;

        public MatchServce(App app)
        {
            this.app = app;
        }

        public Result Cancel(AClient client)
        {
            var player = app.playerRsp.FindPlayer(client);
            lock (app.roomRsp.lcoker)
            {
                var game = app.roomRsp.GetRoom(player.RoomId);
                if (game == null)
                {
                    return ResultDefine.NotInMatching;
                }
                if (game.IsRunning)
                {
                    return ResultDefine.GameStarted;
                }
                game.Leave(player);
                return ResultDefine.Success;
            }
        }

        public Result Match(AClient client)
        {
            var player = app.playerRsp.FindPlayer(client);
            lock (app.roomRsp.lcoker)
            {
                var game = app.roomRsp.GetRoom(player.RoomId);
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
            foreach (var item in app.roomRsp)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return app.roomRsp.NewRoom();
        }
        public Result Play(int x, int y, AClient client)
        {
            return app.playerRsp.FindPlayer(client).Play(x, y);
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
