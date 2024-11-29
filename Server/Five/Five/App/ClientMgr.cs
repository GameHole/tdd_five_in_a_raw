using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class ClientMgr
    {
        private Matching matching;
        public ConcurrentList<Client> clients { get; private set; }
        public ConcurrentDictionary<Client, Matcher> matchers { get; }

        public RequestRegister register = new RequestRegister();
        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            clients = new ConcurrentList<Client>();
            matchers = new ConcurrentDictionary<Client, Matcher>();
        }
        public void Invoke(ASocket socket)
        {
            var client = new Client();
            client.Init(socket);
            
            var matcher = new Matcher(matching);
            matcher.Player.notifier = new NetNotifier(socket, matcher.Player);
            matchers.TryAdd(client, matcher);
            client.matcher = matcher;

            register.Regist(client,this);

            clients.Add(client);

            socket.onClose += () =>
            {
                matcher.Player.OutLine();
                clients.Remove(client);
                matchers.TryRemove(client,out var c);
            };
        }

        public void Stop()
        {
            clients.Clear();
            matchers.Clear();
        }
    }
}
