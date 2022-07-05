namespace Five
{
    public class StartedProcesser : AProcesser<StartNotify>
    {
        private MatchView main;
        private GameView game;
        private PlayersInfo info;
        public StartedProcesser(MatchView main, GameView game, PlayersInfo info)
        {
            this.main = main;
            this.game = game;
            this.info = info;
        }

        public override int OpCode => MessageCode.StartNotify;

        public override void ProcessContent(StartNotify message)
        {
            info.infos = message.infos;
            main.Close();
            game.Open();
        }
    }
}
