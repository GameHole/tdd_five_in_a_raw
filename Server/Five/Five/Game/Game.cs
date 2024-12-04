using System;
using System.Text;

namespace Five
{
    public class Game:IPlayable
    {
        private GameNotifier gameNotifier;
       
        public Turn turn { get; private set; }
        public LoopTimer timer { get; private set; }
        public Chessboard chessboard { get; private set; }
        private IRoom room;
        public Game(IRoom room)
        {
            turn = new Turn(room.maxPlayer);
            timer = new LoopTimer(30);
            chessboard = new Chessboard(15, 15);
            gameNotifier = new GameNotifier(room);
            this.room = room;
        }

        public void Start()
        {
            var chess = 1;
            foreach (var item in room.Players)
            {
                item.Start(chess++);
            }
            turn.Start();
            gameNotifier.NotifyStart();
            TurnPlayer();
            TimerDriver.Start(timer);
            timer.onTime -= NextPlayer;
            timer.onTime += NextPlayer;
        }

        private void TurnPlayer()
        {
            foreach (var item in room.Players)
            {
                if (item.PlayerId == turn.index)
                    item.playable = this;
                else
                    item.playable = new NotTurnPlayable();
            }
            gameNotifier.NotifyTurn(turn.index);
        }

        public void NextPlayer()
        {
            turn.Next();
            TurnPlayer();
            timer.Reset();
        }

        public void Finish(int id)
        {
            gameNotifier.NotifyFinish(id);
            room.Stop();
        }

        public void Stop()
        {
            chessboard.Clear();
            TimerDriver.Stop(timer);
        }

        public Result Play(int x,int y,Player player)
        {
            if (!chessboard.AddValue(x, y, player.chess))
            {
                return ResultDefine.AllReadyHasChess;
            }
            gameNotifier.NotifyPlay(x, y, player.PlayerId);
            if (chessboard.isFiveInRow(player.chess))
            {
                Finish(player.PlayerId);
            }
            else
            {
                NextPlayer();
            }
            return ResultDefine.Success;
        }
    }
}
