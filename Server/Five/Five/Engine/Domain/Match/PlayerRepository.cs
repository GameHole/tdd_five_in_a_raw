using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;

namespace Five
{
    public class IdGenrator
    {
        int seed;
        public virtual int Genrate()
        {
            return Interlocked.Increment(ref seed);
        }
        public virtual int InvailedId { get; }
        public int Seed => seed;
    }
    public class PlayerRepository
    {
        private ConcurrentDictionary<int, Player> players;

        public int Count => players.Count;
        public PlayerRepository()
        {
            players = new ConcurrentDictionary<int, Player>();
        }
        public Player FindPlayer(int id)
        {
            return players[id];
        }

        public void Remove(int id)
        {
            players.TryRemove(id, out var c);
        }

        public void Stop()
        {
            players.Clear();
        }

        public void Add(int id,Player player)
        {
            players.TryAdd(id, player);
        }

        public void Add(Player player)
        {
            players.TryAdd(player.Id, player);
        }
    }
}
