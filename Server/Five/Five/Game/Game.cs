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
        public bool IsRunning { get; private set; }
        private IOutLineable startOut;

        public Game()
        {
            players = new ConcurrentDictionary<int, Player>();
            turn = new Turn(maxPlayer);
            timer = new LoopTimer(30);
            chessboard = new Chessboard(15, 15);
            gameNotifier = new GameNotifier(this);
            startOut = new StartOutLineable(this);
        }

        public bool isFull()
        {
            return PlayerCount >= maxPlayer;
        }

        public bool Join(Player player)
        {
            if (TryDistributeIdentity(out var playerId))
            {
                player.GameId = Id;
                player.PlayerId = playerId;
                players.TryAdd(player.PlayerId, player);

                player.playable = new WaitGamePlayable();
                player.outlineable = new StopOutLineable(this, player);

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


        public Player GetPlayer(int index)
        {
            players.TryGetValue(index, out var player);
            return player;
        }

        public void Remove(Player player)
        {
            if(players.TryRemove(player.PlayerId, out var p))
            {
                Interlocked.Decrement(ref _count);
                player.Reset();
            }
        }

        public void Start()
        {
            TryThrowNotStartException();
            var chess = 1;
            foreach (var item in players.Values)
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
            IsRunning = true;
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
            foreach (var item in players.Values)
            {
                item.Finish();
            }
            stopInternal();
        }

        private void stopInternal()
        {
            chessboard.Clear();
            players.Clear();
            Interlocked.Exchange(ref _count, 0);
            TimerDriver.Stop(timer);
            IsRunning = false;
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
        private void TryThrowNotStartException()
        {
            if (!isFull())
                throw new GameException("No enough player for start");
        }
    }
}
