namespace Five
{
    public class PlayedProcesser : AProcesser<PlayedNotify>
    {
        private ChessboardView chessView;
        private PlayersInfo players;

        public PlayedProcesser(ChessboardView chessView, PlayersInfo players)
        {
            this.chessView = chessView;
            this.players = players;
        }

        public override int OpCode => MessageCode.PlayedNotify;


        public override void ProcessContent(PlayedNotify message)
        {
            chessView.SetChess(message.x, message.y,players.FindChess(message.id));
        }
       
    }
}
