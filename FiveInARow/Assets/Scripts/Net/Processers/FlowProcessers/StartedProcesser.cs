namespace Five
{
    public class StartedProcesser : AFlowProcesser<StartNotify>
    {
        private Player player;

        public StartedProcesser(Player player)
        {
            this.player = player;
        }

        public override int OpCode => MessageCode.StartNotify;

        public override void ProcessContent(StartNotify message)
        {
            players.infos = message.infos;
            int chess = players.FindChess(message.playerId);
            player.chess = chess;
            foreach (var item in flow.GetFlowList<IGameStart>())
            {
                item.Start(chess);
            } 
        }
    }
}
