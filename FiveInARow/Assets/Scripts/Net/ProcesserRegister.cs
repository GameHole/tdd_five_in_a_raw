namespace Five
{
    public class ProcesserRegister:IProcesserRegister
    {
        public MatchView matchView;

        public GameView gameView;
        
        public Player player;
        public PlayersInfo playersInfo;

        public ProcesserRegister(MatchView matchView, GameView gameView, Player player, PlayersInfo playersInfo)
        {
            this.matchView = matchView;
            this.gameView = gameView;
            this.player = player;
            this.playersInfo = playersInfo;
        }

        public virtual void Regist(MessageProcesser processer)
        {
            var loger = UnityEngine.Debug.unityLogger;
            var processers = new IProcesser[]
            {
                new StartedProcesser(matchView,gameView,playersInfo),
                new PlayedProcesser(gameView.chessboard,playersInfo),
                new FinishedProcesser(gameView.finishView,player),
                new ResponseDecorater(new MatchProcesser(matchView,player),loger),
                new ResponseDecorater(new CancelMatchProcesser(matchView),loger),
                new TurnProcesser(gameView.countDown,gameView.chessSelector,player,playersInfo),
                new ResponseDecorater(new NoneResponseProcesser(MessageCode.RequestPlay),loger)
            };
            foreach (var item in processers)
            {
                processer.Processers.Add(item.OpCode, item);
            }
        }
    }
}
