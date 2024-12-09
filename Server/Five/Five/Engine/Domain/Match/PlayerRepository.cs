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
        private ConcurrentDictionary<AClient, Player> matchers;
        private IdGenrator gen;

        public int Count => matchers.Count;
        public PlayerRepository(IdGenrator gen)
        {
            this.gen = gen;
            matchers = new ConcurrentDictionary<AClient, Player>();
        }

        public void Login(AClient socket)
        {
            Player player = NewMatcher(socket);
            player.notifier = new NetNotifier(socket, player);
            Add(socket, player);
        }
        public Player FindPlayer(AClient socket)
        {
            return matchers[socket];
        }
        private Player NewMatcher(AClient client)
        {
            client.Id = gen.Genrate();
            var player = new Player();
            client.onClose += () =>
            {
                player.OutLine();
                client.Id = gen.InvailedId;
                matchers.TryRemove(client, out var c);
            };
            return player;
        }

        public void Stop()
        {
            matchers.Clear();
        }

        public void Add(AClient client,Player player)
        {
            matchers.TryAdd(client, player);
        }
    }
}
