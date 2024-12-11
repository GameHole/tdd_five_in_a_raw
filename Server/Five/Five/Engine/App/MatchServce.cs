using System;

namespace Five
{
    public class MatchServce
    {
        private Domain domain;

        public MatchServce(Domain domain)
        {
            this.domain = domain;
        }

        public Result Cancel(AClient client)
        {
            var player = domain.playerRsp.FindPlayer(client);
            lock (domain.roomRsp.lcoker)
            {
                var game = domain.roomRsp.GetRoom(player.RoomId);
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
            var player = domain.playerRsp.FindPlayer(client);
            lock (domain.roomRsp.lcoker)
            {
                var game = domain.roomRsp.GetRoom(player.RoomId);
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
            foreach (var item in domain.roomRsp)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return domain.roomRsp.NewRoom();
        }
        public Result Commit(PlayRequest request, AClient client)
        {
            return domain.playerRsp.FindPlayer(client).Commit(request);
        }

        public void Stop()
        {
            domain.Stop();
        }

        public void Login(AClient socket)
        {
            domain.Login(socket);
        }
    }
}
