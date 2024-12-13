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
        private IdGenrator gen;

        public int Count => players.Count;
        public PlayerRepository(IdGenrator gen)
        {
            this.gen = gen;
            players = new ConcurrentDictionary<int, Player>();
        }
        public Player FindPlayer(int id)
        {
            return players[id];
        }
        public int Login(INotifier notifier)
        {
            var id = gen.Genrate();
            var player = new Player(id);
            player.notifier = notifier;
            Add(player);
            return id;
        }

        public void OutLine(int id)
        {
            FindPlayer(id).OutLine();
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
