using System;
using System.Text;

namespace Five
{
    public class Game : AGame
    {
        private GameNotifier gameNotifier;
       
        public Turn turn { get; private set; }
        public LoopTimer timer { get; private set; }
        public Chessboard chessboard { get; private set; }
        public Game(int boardSize,float turnTime)
        {
            timer = new LoopTimer(turnTime);
            chessboard = new Chessboard(boardSize, boardSize);
        }
        public override void Init(IRoom room)
        {
            base.Init(room);
            turn = new Turn(room.maxPlayer);
            gameNotifier = new GameNotifier(room);
        }

        public override void Start()
        {
            foreach (var item in room.Players)
            {
                item.playable = this;
            }
            var chess = 1;
            foreach (var item in room.Players)
            {
                item.Start(chess++);
            }
            turn.Start();
            gameNotifier.NotifyStart();
            NotifyTurnPlayer();
            TimerDriver.Start(timer);
            timer.onTime -= NextPlayer;
            timer.onTime += NextPlayer;
        }

        private void NotifyTurnPlayer()
        {
            gameNotifier.NotifyTurn(turn.index);
        }

        public void NextPlayer()
        {
            turn.Next();
            NotifyTurnPlayer();
            timer.Reset();
        }

        public void Finish(int id)
        {
            gameNotifier.NotifyFinish(id);
            room.Stop();
        }

        public override void Stop()
        {
            chessboard.Clear();
            TimerDriver.Stop(timer);
        }

        public override Result Commit(Message message,Player player)
        {
            if (player.PlayerId != turn.index)
                return ResultDefine.NotCurrentTurnPlayer;
            PlayRequest play = message as PlayRequest;
            int x = play.x;
            int y = play.y;
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
