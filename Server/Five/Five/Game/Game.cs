using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Game:IPlayable
    {
        private Dictionary<int,Player> players;
        private GameNotifier gameNotifier;
       
        public readonly int maxPlayer = 2;
        public Turn turn { get; private set; }
        public LoopTimer timer { get; private set; }
        public Chessboard chessboard { get; private set; }
        public int Id;
       
        public int PlayerCount { get => players.Count; }
        public IEnumerable<Player> Players { get => players.Values; }

        public Game()
        {
            players = new Dictionary<int, Player>();
            turn = new Turn(maxPlayer);
            timer = new LoopTimer(30);
            chessboard = new Chessboard(15, 15);
            gameNotifier = new GameNotifier(this);
        }

        public bool isFull()
        {
            return PlayerCount >= maxPlayer;
        }


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
            timer.onTime -= NextPlayer;
            timer.onTime += NextPlayer;
            gameNotifier.NotifyStart();
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

        private void NextPlayer()
        {
            turn.Next();
            TurnPlayer();
        }

        public void Finish(int id)
        {
            foreach (var item in players.Values)
            {
                item.Finish();
            }
            TimerDriver.Stop(timer);
            gameNotifier.NotifyFinish(id);
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
                gameNotifier.NotifyPlay(x, y, player.PlayerId);
                NextPlayer();
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
