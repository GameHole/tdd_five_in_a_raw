using System;
namespace Five
{
    public class TurnProcesser : AProcesser<PlayerIdNotify>
    {
        private CountDownView countDown;
        private ChessSelectorView chessSelector;
        private Player player;
        private PlayersInfo players;

        public TurnProcesser(CountDownView countDown, ChessSelectorView chessSelector, Player player, PlayersInfo players)
        {
            this.countDown = countDown;
            this.chessSelector = chessSelector;
            this.player = player;
            this.players = players;
        }

        public override int OpCode => MessageCode.TurnNotify;

        public override void ProcessContent(PlayerIdNotify message)
        {
            countDown.View.GetComponent<CounrDownChessView>().ChessType =players.FindChess( message.playerId);
            if (message.playerId == player.id)
                chessSelector.Start();
            else
                chessSelector.Stop();
            countDown.Reset();
        }
    }
}
