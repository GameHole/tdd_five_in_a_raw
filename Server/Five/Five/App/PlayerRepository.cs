using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class PlayerRepository
    {
        private ConcurrentDictionary<AClient, Player> matchers;
        public int Count => matchers.Count;
        public PlayerRepository()
        {
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
        private Player NewMatcher(AClient socket)
        {
            var player = new Player();
            socket.onClose += () =>
            {
                player.OutLine();
                matchers.TryRemove(socket, out var c);
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
