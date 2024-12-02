using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class ClientMgr
    {
        public Matching matching { get; }
        public ConcurrentList<Client> clients { get; private set; }
        public ConcurrentDictionary<ASocket, Matcher> matchers { get; }

        public RequestRegister register = new RequestRegister();
        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            clients = new ConcurrentList<Client>();
            matchers = new ConcurrentDictionary<ASocket, Matcher>();
        }
        public void Invoke(ASocket socket)
        {
            var client = new Client();
            client.Init(socket);
            
            var matcher = new Matcher(matching);
            matcher.Player.notifier = new NetNotifier(socket, matcher.Player);
            matchers.TryAdd(socket, matcher);

            register.Regist(client,this);

            clients.Add(client);

            socket.onClose += () =>
            {
                matcher.Player.OutLine();
                clients.Remove(client);
                matchers.TryRemove(socket, out var c);
            };
        }

        public void Stop()
        {
            clients.Clear();
            matchers.Clear();
        }

        public Result Match(ASocket socket)
        {
            var matcher = matchers[socket];
            return matcher.matchable.Match(matcher);
        }

        public Result Cancel(ASocket socket)
        {
            var matcher = matchers[socket];
            return matcher.matchable.Cancel(matcher);
        }
        public Result Play(int x, int y, ASocket sok)
        {
            return matchers[sok].Player.Play(x, y);
        }
    }
}
