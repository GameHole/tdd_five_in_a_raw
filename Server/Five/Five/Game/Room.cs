using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Five
{
    public interface IStopable
    {
        void Stop();
    }
    public interface IRoom: IStopable
    {
        IEnumerable<Player> Players { get; }
        int maxPlayer { get; }
    }
    public class Room: IRoom
    {
        private ConcurrentDictionary<int, Player> players;
        public IEnumerable<Player> Players { get => players.Values; }
        public int Id;
        private int _count = 0;
        public int maxPlayer { get; } = 2;
        public int PlayerCount { get => _count; }
        public bool IsRunning { get; private set; }
        public Game game { get; private set; }
        private IOutLineable startOut;

        public Room()
        {
            players = new ConcurrentDictionary<int, Player>();
            startOut = new StartOutLineable(this);
            game = new Game(this);
        }

        public bool Join(Player player)
        {
            if (TryDistributeIdentity(out var playerId))
            {
                player.RoomId = Id;
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
            var count = Interlocked.Increment(ref _count);
            index = Interlocked.Decrement(ref count);
            if (index >= maxPlayer)
            {
                Interlocked.Decrement(ref _count);
                return false;
            }
            return true;
        }
        public bool isFull()
        {
            return _count >= maxPlayer;
        }
        public void Leave(Player player)
        {
            Remove(player);
            player.CancelMatch();
        }

        public Player GetPlayer(int index)
        {
            players.TryGetValue(index, out var player);
            return player;
        }

        public void Remove(Player player)
        {
            if (players.TryRemove(player.PlayerId, out var p))
            {
                Interlocked.Decrement(ref _count);
                player.Reset();
            }
        }
        public bool ContainPlayer(Player player)
        {
            return players.ContainsKey(player.PlayerId);
        }
        public virtual void Stop()
        {
            foreach (var item in Players)
            {
                item.Reset();
            }
            players.Clear();
            Interlocked.Exchange(ref _count, 0);
            IsRunning = false;
            game.Stop();
        }
        public virtual void Start()
        {
            TryThrowNotStartException();
            IsRunning = true;
            foreach (var item in Players)
            {
                item.outlineable = startOut;
            }
            game.Start();
        }
        private void TryThrowNotStartException()
        {
            if (!isFull())
                throw new GameException("No enough player for start");
        }
    }
}
