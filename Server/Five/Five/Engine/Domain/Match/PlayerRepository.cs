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

        public void Login(AClient client)
        {
            Player player = NewPlayer();
            player.notifier = client;
            client.Id = player.Id;
            client.onClose += () =>
            {
                OutLine(client.Id);
                client.Id = gen.InvailedId;
            };
         
        }
        public Player FindPlayer(int id)
        {
            return players[id];
        }
        public Player NewPlayer()
        {
            var id = gen.Genrate();
            var player = new Player();
            player.Id = id;
            Add(id, player);
            return player;
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
    }
}
