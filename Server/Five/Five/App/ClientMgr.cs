using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class ClientRsp
    {
        public RequestRegister register { get; }

        public MessageProcesser processer { get; }

        public ClientRsp(RequestRegister register)
        {
            this.register = register;
            processer = new MessageProcesser(new OpCodeErrorResponseProcesser());
            register.Regist(processer);
        }
        public void Invoke(ASocket socket)
        {
            socket.onRecv = (message) =>
            {
                processer.Process(socket, message);
            };
        }
    }
    public class ClientMgr
    {
        public Matching matching { get; }
        public ConcurrentDictionary<ASocket, Matcher> matchers { get; }

        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            matchers = new ConcurrentDictionary<ASocket, Matcher>();
        }
        public void Invoke(ASocket socket)
        {
            var matcher = new Matcher();
            matcher.Player.notifier = new NetNotifier(socket, matcher.Player);
            matchers.TryAdd(socket, matcher);

            socket.onClose += () =>
            {
                matcher.Player.OutLine();
                matchers.TryRemove(socket, out var c);
            };
        }

        public void Stop()
        {
            matchers.Clear();
        }

        public Result Match(ASocket socket)
        {
            var matcher = matchers[socket];
            return matching.Match(matcher);
        }

        public Result Cancel(ASocket socket)
        {
            var matcher = matchers[socket];
            return matching.Cancel(matcher);
        }
        public Result Play(int x, int y, ASocket sok)
        {
            return matchers[sok].Player.Play(x, y);
        }
    }
}
