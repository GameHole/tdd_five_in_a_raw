using System;
using System.Text;

namespace Five
{
    public class Game: Room,IPlayable
    {
        public override Game game => this;

        private GameNotifier gameNotifier;
       
        public Turn turn { get; private set; }
        public LoopTimer timer { get; private set; }
        public Chessboard chessboard { get; private set; }
        private IOutLineable startOut;

        public Game()
        {
            turn = new Turn(maxPlayer);
            timer = new LoopTimer(30);
            chessboard = new Chessboard(15, 15);
            gameNotifier = new GameNotifier(this);
            startOut = new StartOutLineable(this);
        }
        public override void Start()
        {
            base.Start();

            GameStart();

        }

        private void GameStart()
        {
            var chess = 1;
            foreach (var item in Players)
            {
                item.Start(chess++);
                item.outlineable = startOut;
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
            foreach (var item in Players)
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
            Stop();
        }
        public override void Stop()
        {
            base.Stop();
            GameStop();
        }

        private void GameStop()
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
