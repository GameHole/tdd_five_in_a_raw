using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Game:IPlayable
    {
        private Dictionary<int,Player> players;
        private readonly int maxPlayer = 2;
        public Turn turn;
        public LoopTimer timer;
        public Chessboard chessboard;
        public int Id;

        public Game()
        {
            players = new Dictionary<int, Player>();
            turn = new Turn(maxPlayer);
            timer = new LoopTimer(30);
            chessboard = new Chessboard(15, 15);
        }

        public bool isFull()
        {
            return PlayerCount >= maxPlayer;
        }

        public event Action<int> onFinish;

        public int PlayerCount { get => players.Count; }

        public void Join(Player player)
        {
            var index = players.Count;
            players.Add(index, player);
            player.GameId = Id;
            player.playable = new WaitGamePlayable();
            player.PlayerId = index;
        }

        public Player GetPlayer(int index)
        {
            players.TryGetValue(index, out var player);
            return player;
        }

        public void Remove(Player player)
        {
            players.Remove(player.PlayerId);
            player.Reset();
        }

        public void Start()
        {
            TryThrowNotStartException();
            var chess = 1;
            foreach (var item in players.Values)
            {
                item.Start(chess++);
            }
            turn.Start();
            TurnPlayer();
            TimerDriver.Start(timer);
            timer.onTime -= nextPlayer;
            timer.onTime += nextPlayer;
        }

        private void TurnPlayer()
        {
            foreach (var item in players.Values)
            {
                if (item.PlayerId == turn.index)
                    item.playable = this;
                else
                    item.playable = new NotTurnPlayable();
            }
        }

        private void nextPlayer()
        {
            turn.Next();
            TurnPlayer();
        }

        void Finish(int id)
        {
            foreach (var item in players.Values)
            {
                item.Finish();
            }
            TimerDriver.Stop(timer);
            onFinish?.Invoke(id);
        }

        public bool ContainPlayer(Player player)
        {
            return players.ContainsKey(player.PlayerId);
        }

        public Result Play(int x,int y,Player player)
        {
            if (!chessboard.AddValue(x, y, player.chess))
            {
                return ResultDefine.AllReadyHasChess;
            }
            if (chessboard.isFiveInRow(player.chess))
            {
                Finish(player.PlayerId);
            }
            else
            {
                nextPlayer();
            }
            return ResultDefine.Success;
        }
        private void TryThrowNotStartException()
        {
            if (!isFull())
                throw new GameException("No enough player for start");
        }
    }
}
