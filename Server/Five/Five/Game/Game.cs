using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Five
{
    public class Game:IPlayable
    {
        private ConcurrentDictionary<int,Player> players;
        private GameNotifier gameNotifier;
       
        public readonly int maxPlayer = 2;
        public Turn turn { get; private set; }
        public LoopTimer timer { get; private set; }
        public Chessboard chessboard { get; private set; }
        public int Id;
        private int _count = 0;
        public int PlayerCount { get => _count; }
        public IEnumerable<Player> Players { get => players.Values; }

        public Game()
        {
            players = new ConcurrentDictionary<int, Player>();
            turn = new Turn(maxPlayer);
            timer = new LoopTimer(30);
            chessboard = new Chessboard(15, 15);
            gameNotifier = new GameNotifier(this);
        }

        public bool isFull()
        {
            return PlayerCount >= maxPlayer;
        }


        public bool Join(Player player)
        {
            if(TryDistributeIdentity(out var playerId))
            {
                player.GameId = Id;
                player.playable = new WaitGamePlayable();
                player.PlayerId = playerId;
                players.TryAdd(player.PlayerId, player);
                return true;
            }
            return false;
        }
        bool TryDistributeIdentity(out int index)
        {
            index = Interlocked.Increment(ref _count) - 1;
            if (index >= maxPlayer)
            {
                Interlocked.Decrement(ref _count);
                return false;
            }
            return true;
        }

        public bool isStarted()
        {
            return isFull();
        }

        public Player GetPlayer(int index)
        {
            players.TryGetValue(index, out var player);
            return player;
        }

        public void Remove(Player player)
        {
            players.TryRemove(player.PlayerId, out var p);
            Interlocked.Decrement(ref _count);
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
            gameNotifier.NotifyFinish(id);
            foreach (var item in players.Values)
            {
                item.Finish();
            }
            stopInternal();
        }

        private void stopInternal()
        {
            players.Clear();
            Interlocked.Exchange(ref _count, 0);
            TimerDriver.Stop(timer);
        }

        public void Stop()
        {
            foreach (var item in players.Values)
            {
                item.Reset();
            }
            stopInternal();
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
