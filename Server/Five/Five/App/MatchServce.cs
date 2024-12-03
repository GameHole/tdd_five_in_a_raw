namespace Five
{
    public class MatchServce
    {
        private MatcherMgr mgr;
        private GameMgr gameRsp;

        public MatchServce(MatcherMgr mgr, GameMgr gameRsp)
        {
            this.mgr = mgr;
            this.gameRsp = gameRsp;
        }

        public Result Cancel(ASocket socket)
        {
            var matcher = mgr.matchers[socket];
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
                game.Leave(matcher);
                return ResultDefine.Success;
            }
        }

        public Result Match(ASocket socket)
        {
            var matcher = mgr.matchers[socket];
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
    }
}
